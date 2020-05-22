using EndGame.Test.Actors;
using EndGame.Test.Events;
using UnityEngine;

namespace EndGame.Test.AI
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
    public class Patrol : AIAction
    {
        public override void DoAction(AIStateController _controller, AIStateData _data)
        {
            PatrolAction(_controller, (PatrolData)_data);
        }

        private void PatrolAction(AIStateController _controller, PatrolData _data)
        {
            PatrolToTargetPoint(_controller.GetOwner, _data);
        }

        private void PatrolToTargetPoint(Actor _actor, PatrolData _data)
        {
            Vector3 currentPosition = _actor.transform.position;
            Vector3 targetPosition = _data.GetPatrolPosition;
            Vector3 directionTowardsPosition = targetPosition - currentPosition;
            directionTowardsPosition.y = 0;

            OnActorCommandReceiveEventArgs args = new OnActorCommandReceiveEventArgs()
            {
                actor = _actor,
                command = ActorCommands.Move,
                value = directionTowardsPosition.normalized
            };

            EventController.PushEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, args);
        }
    }
}
