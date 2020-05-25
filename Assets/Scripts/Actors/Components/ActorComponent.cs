using UnityEngine;

namespace EndGame.Test.Actors
{
    public class ActorComponent : MonoBehaviour
    {
        /// <summary>
        /// Owner of the component.
        /// </summary>
        private Actor owner = null;

        /// <summary>
        /// Returns a reference to the owner.
        /// </summary>
        public Actor GetOwner { get => owner; }

        protected virtual void Awake()
        {
            owner = GetComponent<Actor>();
        }
    }
}
