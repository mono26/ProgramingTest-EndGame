using EndGame.Test.AI;
using EndGame.Test.Events;
using EndGame.Test.Events.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.AI
{
    public class ShootData : AIStateData
    {
        [SerializeField]
        private float shootRange = 3.0f;
        [SerializeField]
        private Actor currentTarget = null;

        // TODO listen to target in shoot range.

        public float GetShootRange { get => shootRange; }

        public Actor GetCurrentTarget { get => currentTarget; }

        private void Start()
        {
            // TODO subscribe to on target in sight.
            EventController.SubscribeToEvent(DecisionEvents.TARGET_IN_SHOOT_RANGE, (args) => OnTargetInShootRange((OnTargetInShootRange)args));
        }

        private void OnTargetInShootRange(OnTargetInShootRange _args)
        {
            if (_args.actor == GetOwner)
            {
                currentTarget = _args.target;
            }
        }

        protected override void AddToStateContoller(AIStateController _controller)
        {
            _controller.AddData(this);
        }
    }
}
