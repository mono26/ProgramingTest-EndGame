using EndGame.Test.Events;
using EndGame.Test.Events.AI;
using UnityEngine;

namespace EndGame.Test.AI
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Wait")]
    public class Wait : AIAction
    {
        public override void DoAction(AIView _controller)
        {
            WaitAction(_controller);
        }

        private void WaitAction(AIView _controller)
        {
            Actor actor = _controller.GetOwner;
            Debug.Log("Actor: " + actor.gameObject.name + " waiting");

            // TODO trigger actor waited event.
            OnWaitedActionEventArgs args = new OnWaitedActionEventArgs()
            {
                actor = actor,
                waitedTime = Time.deltaTime
            };

            EventController.PushEvent(ActionEvents.WAITED_ACTION, args);
        }
    }
}
