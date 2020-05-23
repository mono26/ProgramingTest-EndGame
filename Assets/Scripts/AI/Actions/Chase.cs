using EndGame.Test.Actors;
using EndGame.Test.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.AI
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Chase")]
    public class Chase : AIAction
    {
        public override void DoAction(AIStateController _controller)
        {
            ChaseTarget(_controller);
        }

        private void ChaseTarget(AIStateController _controller)
        {
            Actor actor = _controller.GetOwner;
            ChaseData data = _controller.GetStateData<ChaseData>();

            Vector3 currentPosition = actor.transform.position;
            Vector3 targetPosition = data.GetCurrentTarget.transform.position;
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