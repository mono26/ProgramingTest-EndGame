using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController
{
    private State currentState;
    private StateData currentData;

    private void OnUpdate(Actor _actor)
    {
        currentState.OnUpdate(_actor, currentData);
    }

    public void TransitionToState(State _nextState, StateData _initialData)
    {

    }
}
