using EndGame.Test.Actors;
using EndGame.Test.Events;
using UnityEngine;

namespace EndGame.Test.AI
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Patrol")]
    public class Patrol : AIAction
    {
        public override void DoAction(AIStateController _controller)
        {
            PatrolAction(_controller);
        }

        // TODO patrol and chase are very similar.
        private void PatrolAction(AIStateController _controller)
        {
            Actor actor = _controller.GetOwner;
            PatrolData data = _controller.GetStateData<PatrolData>();

            Vector3 currentPosition = actor.transform.position;
            Vector3 targetPosition = data.GetPatrolPosition;
            Vector3 directionTowardsPosition = targetPosition - currentPosition;
            directionTowardsPosition.y = 0;

            OnActorCommandReceiveEventArgs args = new OnActorCommandReceiveEventArgs()
            {
                actor = actor,
                command = ActorCommands.Move,
                value = directionTowardsPosition.normalized
            };

            EventController.PushEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, args);
        }
    }
}
