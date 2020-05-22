using UnityEngine;

namespace EndGame.Test.AI
{
    public abstract class Decision : ScriptableObject
    {
        [SerializeField]
        private float weight = 0.0f;

        public abstract bool Decide(AIStateController _controller, AIStateData _data);
    }
}
