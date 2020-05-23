using EndGame.Test.Events;
using EndGame.Test.Events.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.AI
{
    public class ChaseData : AIStateData
    {
        [SerializeField]
        private float maxChaseTime;

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

            // TODO subscribe to on target in sight.
            EventController.SubscribeToEvent(DecisionEvents.TARGET_IN_SIGHT, (args) => OnTargetInSight((OnTargetInSightEventArgs)args));
        }

        private void Update()
        {
            currentChaseTime++;
        }

        private void OnTargetInSight(OnTargetInSightEventArgs _args)
        {
            if (_args.actor == GetOwner)
            {
                currentTarget = _args.target;

                Debug.DrawLine(GetOwner.transform.position, _args.target.transform.position, Color.magenta, 3.0f);
            }
        }
    }
}
