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
        public override void DoAction(AIStateController _controller, AIStateData _data)
        {
            ChaseTarget(_controller.GetOwner, (ChaseData)_data);
        }

        private void ChaseTarget(Actor _actor, ChaseData _data)
        {
            Vector3 currentPosition = _actor.transform.position;
            Vector3 targetPosition = _data.GetCurrentTarget.transform.position;
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