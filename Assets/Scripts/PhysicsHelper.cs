using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.Utils
{
    public class PhysicsHelper : MonoBehaviour
    {
        public static RaycastHit CastRayForHits(Vector3 _startPosition, Vector3 _direction, float _distance, LayerMask _layers)
        {
            RaycastHit hit;
            Physics.Raycast(_startPosition, _direction.normalized, out hit, _distance, _layers);
            return hit;
        }
    }
}
