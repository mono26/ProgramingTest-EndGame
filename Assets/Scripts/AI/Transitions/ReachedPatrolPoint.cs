﻿using EndGame.Test.Events;
using EndGame.Test.Events.AI;
using UnityEngine;

namespace EndGame.Test.AI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/ReachedPatrolPoint")]
    public class ReachedPatrolPoint : Decision
    {
        public override bool Decide(AIView _controller)
        {
            return HasReachedPatrolPoint(_controller);
        }

        /// <summary>
        /// Check if the ai has reached the current patrol point, if so send an event.
        /// </summary>
        /// <param name="_controller">Patrolling ai.</param>
        /// <returns></returns>
        private bool HasReachedPatrolPoint(AIView _controller)
        {
            Actor _actor = _controller.GetOwner;
            PatrolData _data = _controller.GetStateData<PatrolData>();

            bool reachedPatrolPoint = false;

            Vector3 currentPosition = _actor.transform.position;
            Vector3 patrolPosition = _data.GetPatrolPosition;
            // So the vector is flat and in the same plane as the character.
            patrolPosition.y = currentPosition.y;
            float sqrdistance = (patrolPosition - currentPosition).sqrMagnitude;

            if (currentPosition.Equals(patrolPosition) || sqrdistance <= 0.1f)
            {
                reachedPatrolPoint = true;

                OnPatrolPointReachedEventArgs args = new OnPatrolPointReachedEventArgs()
                {
                    actor = _actor
                };

                EventController.QueueEvent(DecisionEvents.PATROL_POINT_REACHED, args);
            }

            return reachedPatrolPoint;
        }
    }
}
