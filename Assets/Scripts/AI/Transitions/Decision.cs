using UnityEngine;

namespace EndGame.Test.AI
{
    public abstract class Decision : ScriptableObject
    {
        /// <summary>
        /// Makes a true or false decision, used for AI to transition from one state to another.
        /// </summary>
        /// <param name="_controller"></param>
        /// <returns></returns>
        public abstract bool Decide(AIView _controller);
    }
}
