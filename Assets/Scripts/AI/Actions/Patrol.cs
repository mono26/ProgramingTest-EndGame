using EndGame.Test.Actors;
using EndGame.Test.Events;
using UnityEngine;

namespace EndGame.Test.AI
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
    public class Patrol : AIAction
    {
        public override void DoAction(AIView _controller)
        {
            PatrolAction(_controller);
        }

        // TODO patrol and chase are very similar.
        private void PatrolAction(AIView _controller)
        {
            if (_controller.GetAIData.GetNavigationComponent.isStopped)
            {
                _controller.GetAIData.GetNavigationComponent.isStopped = false;
            }

            PatrolData data = _controller.GetStateData<PatrolData>();
            Vector3 targetPosition = data.GetPatrolPosition;

            OnActorCommandReceiveEventArgs args = new OnActorCommandReceiveEventArgs()
            {
                baseArgs = new OnActorEventEventArgs() { actor = _controller.GetOwner },
                command = ActorCommands.Move,
                value = targetPosition
            };

            EventController.QueueEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, args);
        }
    }
}
