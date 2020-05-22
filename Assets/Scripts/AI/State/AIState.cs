using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.AI
{
    [CreateAssetMenu(menuName = "PluggableAI/State")]
    public class AIState : ScriptableObject
    {
        [SerializeField]
        private string stateId = "";
        [SerializeField]
        private AIAction[] actions = null;
        [SerializeField]
        private Transition[] transitions = null;

        public string GetStateId { get => stateId; }

        public void OnUpdate(AIStateController _controller)
        {
            AIStateData data = _controller.GetStateData(stateId);
            DoActions(_controller, data);
            CheckTransitions(_controller, data);
        }

        private void DoActions(AIStateController _controller, AIStateData _stateData)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].DoAction(_controller, _stateData);
            }
        }

        private void CheckTransitions(AIStateController _controller, AIStateData _stateData)
        {
            List<AIState> trueStates;
            List<AIState> falseStates;
            for (int i = 0; i < transitions.Length; i++)
            {
                bool transition = transitions[i].decision.Decide(_controller, _stateData);

                if (transition)
                {
                    _controller.TransitionToState(transitions[i].trueState);
                }
                else
                {
                    _controller.TransitionToState(transitions[i].falseState);
                }
            }
        }
    }
}
