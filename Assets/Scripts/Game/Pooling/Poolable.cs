using UnityEngine;

namespace EndGame.Test.Game
{
    public abstract class Poolable : MonoBehaviour
    {
        /// <summary>
        /// Creates a instace of a Poolable object.
        /// </summary>
        /// <returns>New instance.</returns>
        public virtual Poolable CreateInstance()
        {
            return Instantiate(this);
        }
        /// <summary>
        /// Gets the unique Id for this IPoolable.
        /// </summary>
        /// <returns>Id to return.</returns>
        public virtual string GetId()
        {
            return gameObject.name.Replace("(Clone)", string.Empty);
        }
        /// <summary>
        /// Called by the Pool when the object exits the pool.
        /// </summary>
        public abstract void PoolExited();

        /// <summary>
        /// Called by the Pool when the object returns to the pool.
        /// </summary>
        public abstract void PoolEntered();

        /// <summary>
        /// Return to the pool.
        /// </summary>
        public void ReturnToPool()
        {
            PoolOfPools.ReturnObjectToPool(this);

            PoolEntered();
        }
    }
}
