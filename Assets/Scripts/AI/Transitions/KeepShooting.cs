using EndGame.Test.Actors;
using EndGame.Test.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepShooting : TargetInShootRange
{
    public override bool Decide(AIStateController _controller, AIStateData _data)
    {
        Actor actor = _controller.GetOwner;
        TargetDetector targeter = actor.GetComponent<TargetDetector>();
        bool keepShooting = IsTargetInShootRange(actor, targeter);
        
        if (!keepShooting)
        {
            OnActorCommandReceiveEventArgs args = new OnActorCommandReceiveEventArgs()
            {
                actor = GetOwner,
                command = ActorCommands.Shoot,
                // Means the shoot button has been released.
                value = 0.0f
            };

            EventController.PushEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, args);
        }

        return base.Decide(_controller, _data);
    }
}
