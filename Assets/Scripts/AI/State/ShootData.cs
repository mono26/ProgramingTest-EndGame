﻿using EndGame.Test.Actors;
using EndGame.Test.AI;
using EndGame.Test.Events;
using EndGame.Test.Events.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace EndGame.Test.AI
{
    public class ShootData : AIStateData
    {
        private Action<IEventArgs> OnTargetInShootRangeListener;

        [SerializeField]
        private float shootRange = 3.0f;
        [SerializeField]
        private Actor currentTarget = null;

        // TODO add targeter component ?

        public float GetShootRange { get => shootRange; }

        public Actor GetCurrentTarget { get => currentTarget; }

        protected override void Start()
        {
            base.Start();

            OnTargetInShootRangeListener = (args) => OnTargetInShootRange((OnTargetInShootRange)args);

            EventController.SubscribeToEvent(DecisionEvents.TARGET_IN_SHOOT_RANGE, OnTargetInShootRangeListener);
        }

        private void OnDestroy()
        {
            EventController.UnSubscribeFromEvent(DecisionEvents.TARGET_IN_SHOOT_RANGE, OnTargetInShootRangeListener);
        }

        private void OnTargetInShootRange(OnTargetInShootRange _args)
        {
            if (_args.actor == GetOwner)
            {
                currentTarget = _args.target;
            }
        }

        protected override void AddToStateContoller(AIView _controller)
        {
            _controller.AddData(this);
        }
    }
}
