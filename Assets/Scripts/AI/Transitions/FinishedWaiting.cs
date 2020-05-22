using EndGame.Test.Events;
using EndGame.Test.Events.AI;
using UnityEngine;

namespace EndGame.Test.AI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/FinishedWaiting")]
    public class FinishedWaiting : Decision
    {
        public override bool Decide(AIStateController _controller, AIStateData _data)
        {
            return HasFinishedWaiting(_controller.GetOwner, (WaitData)_data);
        }

        private bool HasFinishedWaiting(Actor _actor, WaitData _data)
        {
            bool finishedWaiting = false;
            if (_data.GetWaitedTime >= _data.GetMaxTime)
            {
                finishedWaiting = true;

                OnWaitFinishedEventArgs args = new OnWaitFinishedEventArgs()
                {
                    actor = _actor,
                };

                EventController.PushEvent(DecisionEvents.WAIT_FINISH, args);
            }

            return finishedWaiting;
        }
    }
}
