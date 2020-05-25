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

        protected override void Awake()
        {
            base.Awake();

            // Catch component references.
            navigationComponent = GetComponent<NavMeshAgent>();
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
                MoveTowardsTarget(targetPosition);
            }

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
        /// Sets the target position for the navigation agent.
        /// </summary>
        /// <param name="_args"></param>
        protected override void OnActorCommandReceive(OnActorCommandReceiveEventArgs _args)
        {
            if (GetOwner == _args.baseArgs.actor)
            {
                if (_args.command.Equals(ActorCommands.Move))
                {
                    targetPosition = (Vector3)_args.value;
                }
            }
        }

        /// <summary>
        /// Sets the destination of the navigation agent.
        /// </summary>
        /// <param name="_nextPosition"></param>
        protected override void MoveTowardsTargetPosition(Vector3 _nextPosition)
        {
            _nextPosition.y = GetOwner.transform.position.y;
            navigationComponent.SetDestination(_nextPosition);
        }
    }
}
