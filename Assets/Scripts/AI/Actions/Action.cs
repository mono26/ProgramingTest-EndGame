using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.AI
{
    public abstract class AIAction : ScriptableObject
    {
        public abstract void DoAction(AIView _controller);
    }
}
