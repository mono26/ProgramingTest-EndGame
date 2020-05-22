using EndGame.Test.Actors;
using EndGame.Test.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.AI
{
    public class ShootTarget : AIAction
    {
        public override void DoAction(AIStateController _controller, AIStateData _data)
        {
            DoShoot(_controller.GetOwner);
        }

        private void DoShoot(Actor _actor)
        {
            OnActorCommandReceiveEventArgs args = new OnActorCommandReceiveEventArgs()
            {
                actor = _actor,
                command = ActorCommands.Shoot,
                // Means the shoot button is pressed.
                value = 1.0f
            };

            EventController.PushEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, args);
        }
    }
}
