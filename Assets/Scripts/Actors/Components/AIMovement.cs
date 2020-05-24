using EndGame.Test.Actors;
using EndGame.Test.Events;
using UnityEngine;
using UnityEngine.AI;

namespace EndGame.Test.AI
{
    public class AIMovement : Movement
    {
        [SerializeField]
        private NavMeshAgent navigationComponent = null;

        private Vector3 targetPosition = Vector3.zero;

        private void OnDrawGizmos()
        {
            if (GetOwner)
            {
                Gizmos.color = Color.yellow;

                Vector3 startPosition = GetOwner.transform.position;

                Gizmos.DrawLine(startPosition, targetPosition);
            }
        }

        public override void OnAwake(Actor _actor)
        {
            base.OnAwake(_actor);

            // Catch component references.
            navigationComponent = _actor.GetComponent<NavMeshAgent>();
        }

        protected override void Start()
        {
            base.Start();

            navigationComponent.speed = movementSpeed;
        }

        protected override void FixedUpdate()
        {
            Vector3 currentPosition = GetOwner.transform.position;

            if (!targetPosition.Equals(currentPosition))
            {
                //Debug.Log("Posicion before movement: " + GetOwner.transform.position.ToString());

                MoveTowardsTarget(targetPosition);

                //Debug.Log("Actor is moving");
            }

            if (lastPosition.Equals(currentPosition))
            {
                // TODO create base actor event.
                OnActorStoppedMovement args = new OnActorStoppedMovement()
                {
                    actor = GetOwner
                };

                EventController.QueueEvent(ActorEvents.ACTOR_MOVEMENT_STOPPED, args);

                //Debug.Log("Actor is stopped");
            }

            lastPosition = currentPosition;
        }

        protected override void OnActorCommandReceive(OnActorCommandReceiveEventArgs _args)
        {
            if (GetOwner == _args.actor)
            {
                if (_args.command.Equals(ActorCommands.Move))
                {
                    targetPosition = (Vector3)_args.value;
                }
            }
        }

        protected override void MoveTowardsTargetPosition(Vector3 _nextPosition)
        {
            _nextPosition.y = GetOwner.transform.position.y;
            navigationComponent.SetDestination(_nextPosition);
            //bodyComponent.MovePosition(_nextPosition);
        }
    }
}
