using EndGame.Test.Actors;
using EndGame.Test.Events;
using UnityEngine;

namespace EndGame.Test.AI
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/ShootTarget")]
    public class ShootTarget : AIAction
    {
        public override void DoAction(AIStateController _controller)
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
