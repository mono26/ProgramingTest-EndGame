using EndGame.Test.Events;
using EndGame.Test.Events.AI;
using UnityEngine;

namespace EndGame.Test.AI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/FinishedWaiting")]
    public class FinishedWaiting : Decision
    {
        public override bool Decide(AIView _controller)
        {
            return HasFinishedWaiting(_controller);
        }

        private bool HasFinishedWaiting(AIView _controller)
        {
            Actor actor = _controller.GetOwner;
            WaitData data = _controller.GetStateData<WaitData>();

            bool finishedWaiting = false;
            if (data.GetWaitedTime >= data.GetMaxTime)
            {
                finishedWaiting = true;

                OnWaitFinishedEventArgs args = new OnWaitFinishedEventArgs()
                {
                    actor = actor,
                };

                EventController.PushEvent(DecisionEvents.WAIT_FINISH, args);
            }

            return finishedWaiting;
        }
    }
}
