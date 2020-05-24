using EndGame.Test.Actors;
using EndGame.Test.AI;
using EndGame.Test.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.AI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/KeepShooting")]
    public class KeepShooting : TargetInShootRange
    {
        public override bool Decide(AIView _controller)
        {
            bool keepShooting = base.Decide(_controller);

            if (!keepShooting)
            {
                OnActorCommandReceiveEventArgs args = new OnActorCommandReceiveEventArgs()
                {
                    actor = _controller.GetOwner,
                    command = ActorCommands.Shoot,
                    // Means the shoot button has been released.
                    value = 0.0f
                };

                EventController.QueueEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, args);
            }

            return keepShooting;
        }
    }
}
