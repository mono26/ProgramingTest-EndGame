using EndGame.Test.Actors;
using UnityEngine;

namespace EndGame.Test.AI
{
    /// <summary>
    /// Data class that contains actor instance data for a state. Each state should have it's own data.
    /// </summary>
    public abstract class AIStateData : ActorComponent
    {
        protected virtual void Start()
        {
            AddToStateContoller(GetOwner.GetComponent<AIView>());
        }

        protected abstract void AddToStateContoller(AIView _controller);
    }
}
