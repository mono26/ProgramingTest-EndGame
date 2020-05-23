using UnityEngine;

namespace EndGame.Test.Actors
{
    public class ActorComponent : MonoBehaviour
    {
        private Actor owner = null;

        public Actor GetOwner { get => owner; }

        public virtual void OnAwake(Actor _owner)
        {
            owner = _owner;
        }
    }
}
