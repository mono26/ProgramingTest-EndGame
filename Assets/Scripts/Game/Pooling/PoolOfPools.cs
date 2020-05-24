using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.Game
{
    public class PoolOfPools : Singleton<PoolOfPools>
    {
        [SerializeField]
        private List<PoolData> registeredPools = new List<PoolData>();

        private Dictionary<string, Pool> pools = new Dictionary<string, Pool>();

        private void Start()
        {
            InitializePools();
        }

        private void InitializePools()
        {
            if (registeredPools != null && registeredPools.Count > 0)
            {
                foreach (PoolData pool in registeredPools)
                {
                    OnPoolAdded(pool);
                }
            }
        }

        private void OnPoolAdded(PoolData _data)
        {
            if (!pools.ContainsKey(_data.poolable.GetId()))
            {
                Pool poolToAdd = new Pool(_data);
                pools.Add(_data.poolable.GetId(), poolToAdd);
            }
            else
            {
                Debug.LogError("Already registered: " + _data.poolable.GetId());
            }
        }

        public static Poolable GetObjectFromPool(string _id)
        {
            Poolable objectToReturn = null;
            if (GetUniqueInstance.pools.ContainsKey(_id))
            {
                objectToReturn = GetUniqueInstance.pools[_id].GetObjectFromPool();
            }
            else
            {
                Debug.LogError("Not registered: " + _id);
            }

            return objectToReturn;
        }

        public static void ReturnObjectToPool(Poolable _objectToReturn)
        {
            if (_objectToReturn != null)
            {
                if (GetUniqueInstance.pools.ContainsKey(_objectToReturn.GetId()))
                {
                    GetUniqueInstance.pools[_objectToReturn.GetId()].ReturnObjectToPool(_objectToReturn);
                }
                else
                {
                    Debug.LogError("Not registered: " + _objectToReturn.GetId());
                }
            }
        }
    }
}
