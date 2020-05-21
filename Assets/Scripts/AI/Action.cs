using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.AI
{
    public abstract class Action : ScriptableObject
    {
        public abstract void DoAction(Actor actor, StateData _data);
    }
}
