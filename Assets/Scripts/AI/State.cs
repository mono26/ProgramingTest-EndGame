using EndGame.Test.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class AIState : ScriptableObject
{
    [SerializeField]
    private string stateId = "";
    [SerializeField]
    private AIAction[] actions = null;
    [SerializeField]
    private Transition[] transitions = null;

    public void OnUpdate(AIStateController _controller, AIStateData _stateData, float _deltaMS)
    {
        DoActions(_actor, _stateData);
    }

    private void DoActions(AIStateController _controller, AIStateData _stateData, float _deltaMS)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].DoAction(_controller, _stateData, _deltaMS);
        }
    }

    private void CheckTransitions(AIStateController _controller, AIStateData _stateData)
    {
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
