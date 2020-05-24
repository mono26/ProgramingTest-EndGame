using EndGame.Test.Utils;
using UnityEngine;

namespace EndGame.Test.Game
{
    public static class PoolingUtils
    {
        public static string poolablesFolder = "Poolables";

        public static Poolable CreateCopy(Poolable _poolable)
        {
            if (_poolable != null)
            {
                return _poolable.CreateInstance();
            }
            else
            {
                // TODO throw can't make copy of null exception.
                throw new System.Exception();
            }
        }

        public static Poolable CreateCopy(string _poolableId)
        {
            if (_poolableId.HasAValue())
            {

                return CreateCopy(GetPoolableResource(_poolableId));
            }
            else
            {
                // TODO throw can't make copy of null id.
                throw new System.Exception();
            }
        }

        private static Poolable GetPoolableResource(string _resourceId)
        {
            // TODO put inside try/catch ???
            Poolable poolableToReturn = null;
            GameObject poolableGameObject = ResourcesUtils.GetResourceGameObject(_resourceId, poolablesFolder);
            if (poolableGameObject)
            {
                poolableToReturn = poolableGameObject.GetComponent<Poolable>();
                if (!poolableGameObject.HasComponent(out poolableToReturn))
                {
                    // TODO throw found resource but no poolable component exception.
                    Debug.LogError($"Found resource GameObject, but it has no { nameof(Poolable) } component.");
                }
            }
            else
            {
                Debug.LogError($"No resource found for { _resourceId }");
            }
            return poolableToReturn;
        }
    }
}
