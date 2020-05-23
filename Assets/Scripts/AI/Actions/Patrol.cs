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
            Actor actor = _controller.GetOwner;
            PatrolData data = _controller.GetStateData<PatrolData>();

            Vector3 targetPosition = data.GetPatrolPosition;

            OnActorCommandReceiveEventArgs args = new OnActorCommandReceiveEventArgs()
            {
                actor = actor,
                command = ActorCommands.Move,
                value = targetPosition
            };

            EventController.PushEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, args);
        }
    }
}
