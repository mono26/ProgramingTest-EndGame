using EndGame.Test.Actors;
using UnityEngine;

namespace EndGame.Test.AI
{
    /// <summary>
    /// Data class that contains actor instance data for a state. Each state should have it's own data.
    /// </summary>
    public abstract class AIStateData : ActorComponent
    {
        public override void OnAwake(Actor _actor)
        {
            base.OnAwake(_actor);

            AddToStateContoller(_actor.GetComponent<AIStateController>());
        }

        protected abstract void AddToStateContoller(AIStateController _controller);
    }
}
