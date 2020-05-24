using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.Game
{
    public class DontDestroyOnLoad : MonoBehaviour
    {
        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
