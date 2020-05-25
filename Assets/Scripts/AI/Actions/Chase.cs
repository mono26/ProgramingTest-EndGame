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
        public override void DoAction(AIView _controller)
        {
            ChaseTarget(_controller);
        }

        private void ChaseTarget(AIView _controller)
        {
            if (_controller.GetAIData.GetNavigationComponent.isStopped)
            {
                _controller.GetAIData.GetNavigationComponent.isStopped = false;
            }

            ChaseData data = _controller.GetStateData<ChaseData>();
            Vector3 targetPosition = data.GetCurrentTarget.transform.position;

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