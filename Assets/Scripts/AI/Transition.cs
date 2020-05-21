using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : ScriptableObject
{
    public Decision decision;
    public AIState trueState;
    public AIState falseState;
}
