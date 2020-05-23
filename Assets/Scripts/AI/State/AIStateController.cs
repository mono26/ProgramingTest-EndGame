﻿using EndGame.Test.Actors;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.AI
{
    public class AIStateController : ActorComponent
    {
        [SerializeField]
        private AIState startingState = null;
        [SerializeField]
        private AIState remainInState = null;

        [SerializeField]
        private AIState currentState;
        private Dictionary<Type, AIStateData> stateDatas = new Dictionary<Type, AIStateData>();

        public AIState GetRemainState { get => remainInState; }

        public override void OnAwake(Actor _actor)
        {
            base.OnAwake(_actor);

            stateDatas = new Dictionary<Type, AIStateData>();
        }

        private void Start()
        {
            TransitionToState(startingState);
        }

        private void Update()
        {
            currentState.OnUpdate(this);
        }

        public void TransitionToState(AIState _nextState)
        {
            if (currentState != _nextState && _nextState != remainInState)
            {
                Debug.Log("Transition to: " + _nextState.GetStateId);

                currentState = _nextState;
                // OnExitState();
            }
        }

        public void AddData<T>(T _stateData) where T : AIStateData
        {
            if (!stateDatas.ContainsKey(typeof(T)))
            {
                stateDatas[typeof(T)] = _stateData;
            }
        }

        public T GetStateData<T>() where T : AIStateData
        {
            AIStateData dataToReturn = null;
            if (stateDatas.ContainsKey(typeof(T)))
            {
                dataToReturn = stateDatas[typeof(T)];
            }
            else
            {
                Debug.LogError("No data of type: " + typeof(T).Name);
            }

            return dataToReturn as T;
        }
    }
}
