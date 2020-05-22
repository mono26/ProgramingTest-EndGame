using EndGame.Test.Events;
using EndGame.Test.Events.AI;
using UnityEngine;

namespace EndGame.Test.AI
{
    public class PatrolData : AIStateData
    {
        [SerializeField]
        private Transform[] patrolPoints = null;

        private int currentPatrolPoint = 0;

        /// <summary>
        /// Gets the current patrol position.
        /// </summary>
        /// <returns></returns>
        public Vector3 GetPatrolPosition { get => patrolPoints[currentPatrolPoint].position; }

        protected override void Start()
        {
            base.Start();

            EventController.SubscribeToEvent(DecisionEvents.PATROL_POINT_REACHED, (args) => OnPatrolPointReached((OnPatrolPointReachedEventArgs)args));
        }

        /// <summary>
        /// Advances de current patrol point to the next one.
        /// </summary>
        public void OnPatrolPointReached(OnPatrolPointReachedEventArgs _args)
        {
            if (_args.actor == GetOwner)
            {
                currentPatrolPoint++;
                currentPatrolPoint = currentPatrolPoint % patrolPoints.Length;
            }
        }
    }
}
