using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : Action
{
    public override void DoAction(Actor _actor, StateData _data)
    {
        throw new System.NotImplementedException();
    }

    private void PatrolToTargetPoint(Actor _actor, Vector3 _targetPosition)
    {
        Movement movement = _actor.GetComponent<Movement>();
    }
}
