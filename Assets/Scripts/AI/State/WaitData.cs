using EndGame.Test.Events;
using EndGame.Test.Events.AI;
using System;
using UnityEngine;

namespace EndGame.Test.AI
{
    public class WaitData : AIStateData
    {
        private Action<IEventArgs> OnActorWaitedListener;
        private Action<IEventArgs> OnActorFinishedWaitingListener;

        [SerializeField]
        private float maxWaitTime = 3.0f;

        private float waitedTime = 0.0f;

        public float GetMaxTime { get => maxWaitTime; }
        public float GetWaitedTime { get => waitedTime; }

        protected override void Start()
        {
            base.Start();

            OnActorWaitedListener = (args) => OnActorWaited((OnWaitedActionEventArgs)args);
            OnActorFinishedWaitingListener = (args) => OnActorFinishedWaiting((OnWaitFinishedEventArgs)args);

            EventController.SubscribeToEvent(ActionEvents.WAITED_ACTION, OnActorWaitedListener);
            EventController.SubscribeToEvent(DecisionEvents.WAIT_FINISH, OnActorFinishedWaitingListener);
        }

        private void OnDestroy()
        {
            EventController.UnSubscribeFromEvent(ActionEvents.WAITED_ACTION, OnActorWaitedListener);
            EventController.UnSubscribeFromEvent(DecisionEvents.WAIT_FINISH, OnActorFinishedWaitingListener);
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

        protected override void AddToStateContoller(AIView _controller)
        {
            _controller.AddData(this);
        }
    }
}
