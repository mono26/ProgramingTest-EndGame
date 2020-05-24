using EndGame.Test.Actors;
using EndGame.Test.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.AI
{
    public class AIView : ActorView
    {
        [SerializeField]
        private AIState startingState = null;
        [SerializeField]
        private AIState remainInState = null;
        [SerializeField]
        private AIData aiData;

        [SerializeField]
        private AIState currentState;
        private Dictionary<Type, AIStateData> stateDatas = new Dictionary<Type, AIStateData>();

        public AIState GetRemainState { get => remainInState; }
        public AIData GetAIData { get => aiData; }

        protected override void Start()
        {
            base.Start();

            EventController.SubscribeToEvent(ActorEvents.ACTOR_RESPAWN, (args) => OnActorRespawn((OnActorRespawn)args));

            InitAI();
        }

        private void OnDestroy()
        {
            EventController.UnSubscribeFromEvent(ActorEvents.ACTOR_RESPAWN, (args) => OnActorRespawn((OnActorRespawn)args));
        }

        /// <summary>
        /// Initializes the AI in the starting state.
        /// </summary>
        private void InitAI()
        {

            TransitionToState(startingState);
        }

        private void Update()
        {
            currentState.OnUpdate(this);
        }

        /// <summary>
        /// Transitions to a state that is not the same as the current one or the remain state.
        /// </summary>
        /// <param name="_nextState"></param>
        public void TransitionToState(AIState _nextState)
        {
            if (currentState != _nextState && _nextState != remainInState)
            {
                currentState = _nextState;
                // OnExitState();
            }
        }

        /// <summary>
        /// Adds state data to the data map.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="_stateData">State data to add.</param>
        public void AddData<T>(T _stateData) where T : AIStateData
        {
            Type type = typeof(T);
            if (!stateDatas.ContainsKey(type))
            {
                stateDatas[type] = _stateData;
            }
        }

        /// <summary>
        /// Gets a specific type of state data. Returns null if there is not one.
        /// </summary>
        /// <typeparam name="T">Type of state data.</typeparam>
        /// <returns></returns>
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

        /// <summary>
        /// Called when an AI actor dies.
        /// </summary>
        /// <param name="_args">AI actor that died.</param>
        protected override void OnActorDeath(OnActorDeath _args)
        {
            if (GetOwner == _args.actor)
            {
                // TODO send to pull.
                gameObject.SetActive(false);
            }
        }


        private void OnActorRespawn(OnActorRespawn _args)
        {
            if (GetOwner == _args.actor)
            {
                InitAI();
            }
        }
    }
}
