using EndGame.Test.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.Game
{
    public class Pool
    {
        private string uniqueId;

        private const int DEFAULT_POOL_SIZE = 3;

        private List<Poolable> Objects { get; set; }
        private string Id
        {
            get => uniqueId;
            set
            {
                if (!value.HasAValue())
                {
                    Debug.LogError("Creating pool with invalid id.");
                }
                uniqueId = value;
            }
        }

        #region Constructors
        public Pool(PoolData _data) : this(_data.poolable, (uint)_data.initialSize) { }

        public Pool(Poolable _objectToPool) : this(_objectToPool, DEFAULT_POOL_SIZE) { }

        public Pool(Poolable _objectToPool, uint _initialSize)
        {
            Objects = new List<Poolable>((int)_initialSize);
            Id = _objectToPool.GetId();
            InitializePool(_objectToPool, _initialSize);
        }
        #endregion

        private void InitializePool(Poolable _objectToPool, uint _initialSize)
        {
            for (int i = 0; i < _initialSize; i++)
            {
                Objects.Add(PoolingUtils.CreateCopy(_objectToPool));
            }
        }

        public Poolable GetObjectFromPool()
        {
            Poolable objectToReturn;
            int count = Objects.Count;
            if (count > 0)
            {
                objectToReturn = Objects[count - 1];
                Objects.Remove(objectToReturn);
            }
            else
            {
                objectToReturn = PoolingUtils.CreateCopy(Id);
            }
            return objectToReturn;
        }

        public void ReturnObjectToPool(Poolable _objectToReturn)
        {
            if (_objectToReturn != null)
            {
                if (!Objects.Contains(_objectToReturn))
                {
                    Objects.Add(_objectToReturn);
                }
                else
                {
                    Debug.LogError("Object to pool is already in the pool.");
                }
            }
        }
    }
}
