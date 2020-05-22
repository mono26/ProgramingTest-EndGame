using EndGame.Test.Events;
using EndGame.Test.Events.AI;
using UnityEngine;

namespace EndGame.Test.AI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/ReachedPatrolPoint")]
    public class ReachedPatrolPoint : Decision
    {
        public override bool Decide(AIStateController _controller, AIStateData _data)
        {
            return HasReachedPatrolPoint(_controller.GetOwner, (PatrolData)_data);
        }

        private bool HasReachedPatrolPoint(Actor _actor, PatrolData _data)
        {
            bool reachedPatrolPoint = false;

            Vector3 currentPosition = _actor.transform.position;
            Vector3 patrolPosition = _data.GetPatrolPosition;
            float sqrdistance = (patrolPosition - currentPosition).sqrMagnitude;

            if (currentPosition.Equals(patrolPosition) || sqrdistance < 0.1f)
            {
                reachedPatrolPoint = true;

                OnPatrolPointReachedEventArgs args = new OnPatrolPointReachedEventArgs()
                {
                    actor = _actor
                };

                EventController.PushEvent(DecisionEvents.PATROL_POINT_REACHED, args);
            }

            return reachedPatrolPoint;
        }
    }
}
