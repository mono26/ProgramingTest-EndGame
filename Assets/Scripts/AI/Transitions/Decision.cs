using UnityEngine;

namespace EndGame.Test.AI
{
    public abstract class Decision : ScriptableObject
    {
        public abstract bool Decide(AIView _controller);
    }
}
