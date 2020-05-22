using EndGame.Test.Actors;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.AI
{
    public class AIStateController : ActorComponent
    {
        [SerializeField]
        private AIState startingState = null;

        [SerializeField]
        private AIState currentState;
        private Dictionary<string, AIStateData> stateDatas = null;

        public override void OnAwake(Actor _actor)
        {
            base.OnAwake(_actor);

            stateDatas = new Dictionary<string, AIStateData>();
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
            if (currentState != _nextState)
            {
                Debug.Log("Transition to: " + _nextState.GetStateId);

                currentState = _nextState;
                // OnExitState();
            }
        }

        public void AddData(string _dataId, AIStateData _stateData)
        {
            if (!stateDatas.ContainsKey(_dataId))
            {
                stateDatas[_dataId] = _stateData;
            }
        }

        public AIStateData GetStateData(string _dataId)
        {
            AIStateData dataToReturn = null;
            if (stateDatas.ContainsKey(_dataId))
            {
                dataToReturn = stateDatas[_dataId];
            }

            return dataToReturn;
        }
    }
}
