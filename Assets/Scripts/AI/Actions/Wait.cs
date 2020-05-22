using EndGame.Test.Events;
using EndGame.Test.Events.AI;
using UnityEngine;

namespace EndGame.Test.AI
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/Wait")]
    public class Wait : AIAction
    {
        public override void DoAction(AIStateController _controller, AIStateData _data)
        {
            WaitAction(_controller.GetOwner);
        }

        private void WaitAction(Actor _actor)
        {
            Debug.Log("Actor: " + _actor.gameObject.name + " waiting");

            // TODO trigger actor waited event.
            var args = new OnWaitedActionEventArgs()
            {
                actor = _actor,
                waitedTime = Time.deltaTime
            };

            EventController.PushEvent(ActionEvents.WAITED_ACTION, args);
        }
    }
}
