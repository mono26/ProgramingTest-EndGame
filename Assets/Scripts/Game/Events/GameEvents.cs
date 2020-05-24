using EndGame.Test.Events;

namespace EndGame.Test.Game
{
    public class GameEvents
    {
        public const string PLAYER_DEATH = "event.game.player.death";
        public const string PLAYER_WON = "event.game.player.won";
        public const string PLAYER_HAS_NO_KEY = "evet.game.player.no.key";
    }

    public struct OnPlayerDeathEventArgs : IEventArgs
    {

    }

    public struct OnPlayerWonEventArgs : IEventArgs
    {

    }

    public struct OnPlayerHasNoKeyEventArgs : IEventArgs
    {

    }
}
