using EndGame.Test.Actors;
using EndGame.Test.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.AI
{
    public class AIView : ActorComponent
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
        }

        private void Start()
        {
            TransitionToState(startingState);

            EventController.SubscribeToEvent(ActorEvents.ACTOR_DEATH, (args) => OnActorDeath((OnActorDeath)args));
        }

        private void Update()
        {
            currentState.OnUpdate(this);
        }

        public void TransitionToState(AIState _nextState)
        {
            if (currentState != _nextState && _nextState != remainInState)
            {
                Debug.Log("Transition to: " + _nextState.name);

                currentState = _nextState;
                // OnExitState();
            }
        }

        public void AddData<T>(T _stateData) where T : AIStateData
        {
            Type type = typeof(T);
            if (!stateDatas.ContainsKey(type))
            {
                stateDatas[type] = _stateData;
            }
        }

        public T GetStateData<T>() where T : AIStateData
        {
            AIStateData dataToReturn = null;
            Type type = typeof(T);
            if (stateDatas.ContainsKey(type))
            {
                dataToReturn = stateDatas[type];
            }
            else
            {
                Debug.LogError("No data of type: " + typeof(T).Name);
            }

            return dataToReturn as T;
        }

        private void OnActorDeath(OnActorDeath _args)
        {
            if (GetOwner == _args.actor)
            {
                // TODO send to pull.
                gameObject.SetActive(false);
            }
        }
    }
}
