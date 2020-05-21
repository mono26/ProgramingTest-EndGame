using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateController
{
    private Actor owner;

    private AIState currentState;
    private AIStateData currentData;

    public Actor GetOwner { get => owner; }

    private void OnUpdate(float _deltaMS)
    {
        currentState.OnUpdate(this, currentData, _deltaMS);
    }

    public void TransitionToState(AIState _nextState)
    {
        if (currentState != _nextState)
        {
            currentState = _nextState;
            // OnExitState();
        }
    }
}
