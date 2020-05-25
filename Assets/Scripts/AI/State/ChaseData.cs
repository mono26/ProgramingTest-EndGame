using EndGame.Test.Actors;
using EndGame.Test.Events;
using EndGame.Test.Events.AI;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace EndGame.Test.AI
{
    public class ChaseData : AIStateData
    {
        private Action<IEventArgs> OnTargetInSightListener;
        private Action<IEventArgs> OnTargetInShootRangeListener;

        /// <summary>
        /// Max duration of a chase after loosing the target.
        /// </summary>
        [SerializeField]
        private float maxChaseTime;
        [SerializeField]
        private Actor currentTarget = null;

        private float currentChaseTime = 0.0f;

        /// <summary>
        /// Gets the current patrol position.
        /// </summary>
        /// <returns></returns>
        public Actor GetCurrentTarget { get => currentTarget; }
        public float GetMaxChaseTime { get => maxChaseTime; }
        public float GetCurrentChaseTime { get => currentChaseTime; }

        protected override void Start()
        {
            base.Start();

            OnTargetInSightListener = (args) => OnTargetInSight((OnTargetInSightEventArgs)args);
            OnTargetInShootRangeListener = (args) => OnTargetInShootRange((OnTargetInShootRange)args);

            EventController.SubscribeToEvent(DecisionEvents.TARGET_IN_SIGHT, OnTargetInSightListener);
            EventController.SubscribeToEvent(DecisionEvents.TARGET_IN_SHOOT_RANGE, OnTargetInShootRangeListener);
        }

        private void OnDestroy()
        {
            EventController.UnSubscribeFromEvent(DecisionEvents.TARGET_IN_SIGHT, OnTargetInSightListener);
            EventController.UnSubscribeFromEvent(DecisionEvents.TARGET_IN_SHOOT_RANGE, OnTargetInShootRangeListener);
        }

        private void Update()
        {
            currentChaseTime++;
        }

        /// <summary>
        /// Sets the current chase target to the target in sight.
        /// </summary>
        /// <param name="_args">Target in sight args.</param>
        private void OnTargetInSight(OnTargetInSightEventArgs _args)
        {
            if (_args.actor == GetOwner)
            {
                currentChaseTime = 0;

                currentTarget = _args.target;
            }
        }

        /// <summary>
        /// Resets the chase time every time we can shoot a target.
        /// </summary>
        /// <param name="_args">Target in shoot range args.</param>
        private void OnTargetInShootRange(OnTargetInShootRange _args)
        {
            if (_args.actor == GetOwner)
            {
                // Reset timer every time we can shoot.
                currentChaseTime = 0;
            }
        }

        protected override void AddToStateContoller(AIView _controller)
        {
            _controller.AddData(this);
        }
    }
}
