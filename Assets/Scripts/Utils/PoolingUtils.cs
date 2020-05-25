using EndGame.Test.Utils;
using System;
using UnityEngine;

namespace EndGame.Test.Game
{
    public static class PoolingUtils
    {
        public static string poolablesFolder = "Poolables";

        /// <summary>
        /// Create a copy of a poolable prefab.
        /// </summary>
        /// <param name="_poolable">Poolable prefab</param>
        /// <returns></returns>
        public static Poolable CreateCopy(Poolable _poolable)
        {
            if (_poolable != null)
            {
                return _poolable.CreateInstance();
            }
            else
            {
                throw new NullReferenceException("There is no poolable to copy!");
            }
        }

        /// <summary>
        /// Create a copy of a prefab by its id.
        /// </summary>
        /// <param name="_poolableId">Id of the prefab to copy.</param>
        /// <returns></returns>
        public static Poolable CreateCopy(string _poolableId)
        {
            if (_poolableId.HasAValue())
            {

                return CreateCopy(GetPoolableResource(_poolableId));
            }
            else
            {
                throw new NullReferenceException("There is no poolable to copy!");
            }
        }

        /// <summary>
        /// Gets a poolable prefab from the resources folder.
        /// </summary>
        /// <param name="_resourceId">Id of the resource.</param>
        /// <returns></returns>
        private static Poolable GetPoolableResource(string _resourceId)
        {
            Poolable poolableToReturn = null;
            GameObject poolableGameObject = ResourcesUtils.GetResourceGameObject(_resourceId, poolablesFolder);
            if (poolableGameObject)
            {
                poolableToReturn = poolableGameObject.GetComponent<Poolable>();
                if (!poolableGameObject.HasComponent(out poolableToReturn))
                {
                    Debug.LogError($"Found resource GameObject, but it has no { nameof(Poolable) } component.");
                }
            }
            else
            {
                throw new NullReferenceException($"No resource found for { _resourceId }");
            }
            return poolableToReturn;
        }
    }
}
