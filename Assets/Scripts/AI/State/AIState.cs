using System.Collections.Generic;
using System.Linq;
using UnityEditorInternal;
using UnityEngine;

namespace EndGame.Test.AI
{
    [CreateAssetMenu(menuName = "PluggableAI/State")]
    public class AIState : ScriptableObject
    {
        [SerializeField]
        private string stateId = "";
        /// <summary>
        /// Controlls the state priority when evaluating multiple transitions. Heigher weight means heigher priority.
        /// </summary>
        [SerializeField]
        private int weight = 0;
        [SerializeField]
        private AIAction[] actions = null;
        [SerializeField]
        private Transition[] transitions = null;

        public string GetStateId { get => stateId; }

        public void OnUpdate(AIStateController _controller)
        {
            DoActions(_controller);
            CheckTransitions(_controller);
        }

        private void DoActions(AIStateController _controller)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                actions[i].DoAction(_controller);
            }
        }

        private void CheckTransitions(AIStateController _controller)
        {
            List<AIState> trueStates = new List<AIState>();
            List<AIState> falseStates = new List<AIState>();
            for (int i = 0; i < transitions.Length; i++)
            {
                bool transition = transitions[i].decision.Decide(_controller);

                if (transition)
                {
                    //_controller.TransitionToState(transitions[i].trueState);
                    trueStates.Add(transitions[i].trueState);
                }
                else
                {
                    //_controller.TransitionToState(transitions[i].falseState);
                    falseStates.Add(transitions[i].falseState);
                }
            }

            int trueWeight = 0;
            trueStates.ForEach((state) => trueWeight += state.weight);
            int falseWeight = 0;
            falseStates.ForEach((state) => falseWeight += state.weight);

            AIState stateToTransition = _controller.GetRemainState;
            if (trueWeight >= falseWeight)
            {
                int maxWeight = 0;
                foreach (AIState state in trueStates)
                {
                    if (state.weight > maxWeight)
                    {
                        maxWeight = state.weight;
                        stateToTransition = state;
                    }
                }
            }
            else
            {
                int maxWeight = 0;
                foreach (AIState state in falseStates)
                {
                    if (state.weight > maxWeight)
                    {
                        maxWeight = state.weight;
                        stateToTransition = state;
                    }
                }
            }

            _controller.TransitionToState(stateToTransition);
        }
    }
}
