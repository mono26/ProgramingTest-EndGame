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
            OnWaitedActionEventArgs args = new OnWaitedActionEventArgs()
            {
                actor = _controller.GetOwner,
                waitedTime = Time.deltaTime
            };

            EventController.QueueEvent(ActionEvents.WAITED_ACTION, args);
        }
    }
}
