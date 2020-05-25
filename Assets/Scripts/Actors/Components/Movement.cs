using EndGame.Test.Events;
using System;
using UnityEngine;

namespace EndGame.Test.Actors
{
    public class Movement : ActorComponent
    {
        private Action<IEventArgs> OnActorCommandListener;

        /// <summary>
        /// Rogodbody to move.
        /// </summary>
        [SerializeField]
        private Rigidbody bodyComponent = null;
        /// <summary>
        /// Speed of movement.
        /// </summary>
        [SerializeField]
        protected float movementSpeed = 3.0f;

        private Vector3 targetDirection = Vector3.zero;
        protected Vector3 lastPosition = Vector3.zero;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;

            Vector3 startPosition = transform.position;
            startPosition.y += 0.5f;
            Vector3 targetPosition = startPosition + targetDirection;

            Gizmos.DrawLine(startPosition, targetPosition);
        }

        protected override void Awake()
        {
            base.Awake();

            // Catch component references.
            bodyComponent = GetComponent<Rigidbody>();
        }

        protected virtual void Start()
        {
            OnActorCommandListener = (args) => OnActorCommandReceive((OnActorCommandReceiveEventArgs)args);

            EventController.SubscribeToEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, OnActorCommandListener);

            lastPosition = GetOwner.transform.position;
        }

        private void OnDestroy()
        {
            EventController.UnSubscribeFromEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, OnActorCommandListener);
        }

        protected virtual void FixedUpdate()
        {
            Vector3 currentPosition = GetOwner.transform.position;

            // Only move if there is movement direction different from zero.
            if (!targetDirection.Equals(Vector3.zero))
            {
                Vector3 nextPosition = currentPosition + targetDirection * movementSpeed * Time.fixedDeltaTime;
                MoveTowardsTarget(nextPosition);
                targetDirection = Vector3.zero;
            }

            // Update position after movement.
            currentPosition = GetOwner.transform.position;

            if (lastPosition.Equals(currentPosition))
            {
                OnActorEventEventArgs args = new OnActorEventEventArgs()
                {
                    actor = GetOwner
                };

                EventController.QueueEvent(ActorEvents.ACTOR_MOVEMENT_STOPPED, args);
            }

            lastPosition = currentPosition;
        }

        /// <summary>
        /// Sets the target direction when there is a movement commmand.
        /// </summary>
        /// <param name="_args"></param>
        protected virtual void OnActorCommandReceive(OnActorCommandReceiveEventArgs _args)
        {
            if (GetOwner == _args.baseArgs.actor)
            {
                if (_args.command.Equals(ActorCommands.Move))
                {
                    targetDirection = (Vector3)_args.value;
                }
            }
        }

        /// <summary>
        /// Moves the actor towards the target position and sends a movement event.
        /// </summary>
        /// <param name="_nextPosition"></param>
        protected void MoveTowardsTarget(Vector3 _nextPosition)
        {
            Vector3 directionVector = _nextPosition - GetOwner.transform.position;

            MoveTowardsTargetPosition(_nextPosition);

            OnActorMovement args = new OnActorMovement()
            {
                baseArgs = new OnActorEventEventArgs() { actor = GetOwner },
                movementDirection = directionVector
            };

            EventController.QueueEvent(ActorEvents.ACTOR_MOVEMENT, args);
        }

        /// <summary>
        /// Move towards the position.
        /// </summary>
        /// <param name="_nextPosition"></param>
        protected virtual void MoveTowardsTargetPosition(Vector3 _nextPosition)
        {
            bodyComponent.MovePosition(_nextPosition);
        }
    }
}
