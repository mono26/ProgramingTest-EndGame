using EndGame.Test.Actors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.AI
{
    public class AIData : ActorComponent
    {
        [SerializeField]
        private float viewDistance;
        [SerializeField]
        private float viewAngle;
    }
}
