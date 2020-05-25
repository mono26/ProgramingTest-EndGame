using EndGame.Test.Events;
using EndGame.Test.Game;

namespace EndGame.Test.Triggers
{
    public class VictoryTrigger : ActionTrigger
    {
        /// <summary>
        /// Triggers a player won event if the actor is the player.
        /// </summary>
        /// <param name="_actor"></param>
        protected override void OnActorEnter(Actor _actor)
        {
            if (_actor.CompareTag("Player"))
            {
                OnPlayerWonEventArgs args = new OnPlayerWonEventArgs()
                {

                };

                EventController.QueueEvent(GameEvents.PLAYER_WON, args);
            }
        }
    }
}
