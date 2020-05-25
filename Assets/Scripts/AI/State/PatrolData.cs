using EndGame.Test.Actors;
using EndGame.Test.Events;
using EndGame.Test.Events.AI;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace EndGame.Test.AI
{
    public class PatrolData : AIStateData
    {
        private Action<IEventArgs> OnPatrolPointReachedListener;

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

            OnPatrolPointReachedListener = (args) => OnPatrolPointReached((OnPatrolPointReachedEventArgs)args);

            EventController.SubscribeToEvent(DecisionEvents.PATROL_POINT_REACHED, OnPatrolPointReachedListener);
        }

        private void OnDestroy()
        {
            EventController.UnSubscribeFromEvent(DecisionEvents.PATROL_POINT_REACHED, OnPatrolPointReachedListener);
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

        protected override void AddToStateContoller(AIView _controller)
        {
            _controller.AddData(this);
        }
    }
}
