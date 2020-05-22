using EndGame.Test.Actors;
using EndGame.Test.AI;
using EndGame.Test.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.AI
{
    public class TargetInShootRange : Decision
    {
        [SerializeField]
        private LayerMask rayCastLayers;

        public override bool Decide(AIStateController _controller, AIStateData _data)
        {
            Actor actor = _controller.GetOwner;
            TargetDetector targeter = actor.GetComponent<TargetDetector>();
            return IsTargetInShootRange(_controller.GetOwner, targeter);
        }

        protected bool IsTargetInShootRange(Actor _actor, TargetDetector _targeter)
        {
            bool inRange = false;

            //TODO use weapon range instead?

            Vector3 startPosition = _actor.GetCenterOfBodyPosition;
            Vector3 directionToTarget = _targeter.GetCurrentTargetDirection;
            float viewDistance = _targeter.GetViewDistance;

            RaycastHit hit = PhysicsHelper.CastRayForHits(startPosition, directionToTarget, viewDistance, rayCastLayers);

            if (hit.collider)
            {
                Actor hitTarget = hit.collider.GetComponent<Actor>();
                if (_targeter.GetCurrenTarget == hitTarget)
                {
                    Vector3 directionToHit = hit.collider.transform.position - _actor.transform.position;
                    inRange = directionToHit.sqrMagnitude <= viewDistance * viewDistance;
                }
            }

            return inRange;
        }
    }
}
