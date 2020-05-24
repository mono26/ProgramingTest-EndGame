using EndGame.Test.Events;
using EndGame.Test.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.Triggers
{
    public class VictoryTrigger : ActionTrigger
    {
        protected override void OnActorEnter(Actor _actor)
        {
            OnPlayerWonEventArgs args = new OnPlayerWonEventArgs()
            {

            };

            EventController.PushEvent(GameEvents.PLAYER_WON, args);
        }
    }
}
