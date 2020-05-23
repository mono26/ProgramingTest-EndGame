using EndGame.Test.Events;
using UnityEngine;

namespace EndGame.Test.Actors
{
    public class Movement : ActorComponent
    {
        [SerializeField]
        private Rigidbody bodyComponent = null;
        [SerializeField]
        protected float movementSpeed = 3.0f;

        private Vector3 targetDirection = Vector3.zero;
        protected Vector3 lastPosition = Vector3.zero;

        private int frame = 0;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            Vector3 startPosition = transform.position;
            startPosition.y += 0.5f;
            Vector3 targetPosition = startPosition + targetDirection;

            Gizmos.DrawLine(startPosition, targetPosition);
        }

        public override void OnAwake(Actor _actor)
        {
            base.OnAwake(_actor);

            // Catch component references.
            bodyComponent = _actor.GetComponent<Rigidbody>();
        }

        protected virtual void Start()
        {
            EventController.SubscribeToEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, (args) => OnActorCommandReceive((OnActorCommandReceiveEventArgs)args));

            lastPosition = GetOwner.transform.position;
        }

        protected virtual void FixedUpdate()
        {
            Vector3 currentPosition = GetOwner.transform.position;

            if (!targetDirection.Equals(Vector3.zero))
            {
                //Debug.Log("Posicion before movement: " + GetOwner.transform.position.ToString());

                Vector3 nextPosition = currentPosition + targetDirection * movementSpeed * Time.fixedDeltaTime;

                MoveTowardsTarget(nextPosition);

                targetDirection = Vector3.zero;

                //Debug.Log("Actor is moving");
            }

            // Update position after movement.
            currentPosition = GetOwner.transform.position;

            //Debug.Log("Posicion before movement: " + GetOwner.transform.position.ToString());

            if (lastPosition.Equals(currentPosition))
            {
                // TODO create base actor event.
                OnActorStoppedMovement args = new OnActorStoppedMovement()
                {
                    actor = GetOwner
                };

                EventController.PushEvent(ActorEvents.ACTOR_MOVEMENT_STOPPED, args);

                //Debug.Log("Actor is stopped");
            }

            lastPosition = currentPosition;
            frame++;
        }

        protected virtual void OnActorCommandReceive(OnActorCommandReceiveEventArgs _args)
        {
            if (GetOwner == _args.actor)
            {
                if (_args.command.Equals(ActorCommands.Move))
                {
                    targetDirection = (Vector3)_args.value;
                }
            }
        }

        protected void MoveTowardsTarget(Vector3 _nextPosition)
        {
            Vector3 directionVector = _nextPosition - GetOwner.transform.position;

            MoveTowardsTargetPosition(_nextPosition);

            OnActorMovement args = new OnActorMovement()
            {
                actor = GetOwner,
                direction = directionVector
            };

            EventController.PushEvent(ActorEvents.ACTOR_MOVEMENT, args);
        }

        protected virtual void MoveTowardsTargetPosition(Vector3 _nextPosition)
        {
            bodyComponent.MovePosition(_nextPosition);
        }
    }
}
