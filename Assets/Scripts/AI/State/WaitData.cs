using EndGame.Test.Events;
using EndGame.Test.Events.AI;
using UnityEngine;

namespace EndGame.Test.AI
{
    public class WaitData : AIStateData
    {
        [SerializeField]
        private float maxWaitTime = 3.0f;

        private float waitedTime = 0.0f;

        public float GetMaxTime { get => maxWaitTime; }
        public float GetWaitedTime { get => waitedTime; }

        private void Start()
        {
            EventController.SubscribeToEvent(ActionEvents.WAITED_ACTION, (args) => OnActorWaited((OnWaitedActionEventArgs)args));
            EventController.SubscribeToEvent(DecisionEvents.WAIT_FINISH, (args) => OnActorFinishedWaiting((OnWaitFinishedEventArgs)args));
        }

        private void OnActorWaited(OnWaitedActionEventArgs _args)
        {
            if (_args.actor == GetOwner)
            {
                waitedTime += _args.waitedTime;
            }
        }

        private void OnActorFinishedWaiting(OnWaitFinishedEventArgs _args)
        {
            if (_args.actor == GetOwner)
            {
                waitedTime = 0;
            }
        }

        protected override void AddToStateContoller(AIStateController _controller)
        {
            _controller.AddData(this);
        }
    }
}
