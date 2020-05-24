using EndGame.Test.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.Game
{
    public class GameEvents
    {
        public const string PLAYER_DEATH = "event.game.player.death";
        public const string PLAYER_WON = "event.game.player.won";
    }

    public struct OnPlayerDeathEventArgs : IEventArgs
    {

    }

    public struct OnPlayerWonEventArgs : IEventArgs
    {

    }
}
