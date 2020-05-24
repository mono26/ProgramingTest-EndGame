using System;

namespace EndGame.Test.Game
{
    /// <summary>
    /// Contains initialization information for a specific pool.
    /// </summary>
    [Serializable]
    public struct PoolData
    {
        public Poolable poolable;
        public int initialSize;
    }
}
