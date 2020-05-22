using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T uniqueInstance;

        protected static T GetUniqueInstance { get => uniqueInstance; }

        protected virtual void Awake()
        {
            if (uniqueInstance == null)
            {
                uniqueInstance = this as T;
            }
            else
            {
                Destroy(this);
            }
        }
    }
}
