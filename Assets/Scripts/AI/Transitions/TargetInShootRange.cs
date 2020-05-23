using EndGame.Test.Actors;
using UnityEngine;

namespace EndGame.Test.AI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/TargetInShootRange")]
    public class TargetInShootRange : TargetInSight
    {
        public override bool Decide(AIStateController _controller, AIStateData _data)
        {
            Actor actor = _controller.GetOwner;
            Detector targeter = actor.GetComponent<Detector>();
            return IsTargetInShootRange(_controller.GetOwner, targeter);
        }

        protected bool IsTargetInShootRange(Actor _actor, Detector _targeter)
        {
            bool inRange = false;

            //TODO use weapon range instead?

            Vector3 startPosition = _actor.GetCenterOfBodyPosition;
            Vector3 directionToTarget = _targeter.GetTargetDirection;
            float viewDistance = _targeter.GetViewDistance;

            Actor hitTarget = RayScan(startPosition, directionToTarget.normalized, viewDistance, rayCastLayers);
            if (_targeter.GetCurrenTarget == hitTarget)
            {
                Vector3 directionToHit = hitTarget.transform.position - _actor.transform.position;
                inRange = directionToHit.sqrMagnitude <= viewDistance * viewDistance;

                Debug.DrawLine(startPosition, hitTarget.transform.position, Color.green, 3.0f);
            }

            return inRange;
        }
    }
}
