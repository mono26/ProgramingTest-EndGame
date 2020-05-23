using EndGame.Test.Actors;
using EndGame.Test.Events;
using EndGame.Test.Events.AI;
using UnityEngine;

namespace EndGame.Test.AI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/TargetInShootRange")]
    public class TargetInShootRange : TargetInSight
    {
        public override bool Decide(AIStateController _controller)
        {
            return IsTargetInShootRange(_controller);
        }

        protected bool IsTargetInShootRange(AIStateController _controller)
        {
            bool inRange = false;

            //TODO use weapon range instead?
            Actor actor = _controller.GetOwner;
            // TODO remove detector from here and other places.
            Detector targeter = actor.GetComponent<Detector>();

            Vector3 startPosition = actor.GetCenterOfBodyPosition;
            Vector3 directionToTarget = targeter.GetTargetDirection;
            float viewDistance = targeter.GetViewDistance;

            Actor hitTarget = RayScan(startPosition, directionToTarget.normalized, viewDistance, rayCastLayers);
            // TODO use chase targe data.
            if (targeter.GetCurrenTarget == hitTarget)
            {
                ShootData data = _controller.GetStateData<ShootData>();

                Vector3 directionToHit = hitTarget.transform.position - actor.transform.position;
                inRange = directionToHit.sqrMagnitude <= data.GetShootRange * data.GetShootRange;

                Debug.DrawLine(startPosition, hitTarget.transform.position, Color.green, 3.0f);

                OnTargetInShootRange args = new OnTargetInShootRange()
                {
                    actor = actor,
                    target = hitTarget
                };

                EventController.PushEvent(DecisionEvents.TARGET_IN_SHOOT_RANGE, args);
                // TODO fire target in shoot range event.
            }

            return inRange;
        }
    }
}
