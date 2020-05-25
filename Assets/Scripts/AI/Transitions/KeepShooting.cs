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

            // If we can't keep shooting send a shoot command with a input value of 0.0f.
            if (!keepShooting)
            {
                OnActorCommandReceiveEventArgs args = new OnActorCommandReceiveEventArgs()
                {
                    baseArgs = new OnActorEventEventArgs() { actor = _controller.GetOwner },
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
