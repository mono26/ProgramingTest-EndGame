using EndGame.Test.Actors;
using UnityEngine;

namespace EndGame.Test.AI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/TargetInSight")]
    public class TargetInSight : Decision
    {
        // TODO check if is best to use actor instead AIStateController.
        public override bool Decide(AIStateController _controller, AIStateData _data)
        {
            Actor actor = _controller.GetOwner;
            TargetDetector targeter = actor.GetComponent<TargetDetector>();
            return HasTargetInSight(actor, targeter);
        }

        private bool HasTargetInSight(Actor _actor, TargetDetector _targeter)
        {
            bool hasTarget = false;
            foreach(Actor target in _targeter.GetNearTargets)
            {
                if (IsTargetInFieldOfView(_actor, target, _targeter.GetViewAngle))
                {
                    if (IsInSight(_actor, target, _targeter.GetViewDistance))
                    {
                        hasTarget = true;
                        break;
                    }
                }
            }

            return hasTarget;
        }

        private bool IsTargetInFieldOfView(Actor _actor, Actor _target, float _viewAngle)
        {
            bool isInFieldOfView = false;

            Vector3 directionToTarget = _target.transform.position - _actor.transform.position;
            float dotProduct = Vector3.Dot(_actor.transform.forward, directionToTarget.normalized);
            if (dotProduct >= Mathf.Cos(_viewAngle / 2))
            {
                isInFieldOfView = true;
            }
            return isInFieldOfView;
        }

        private bool IsInSight(Actor _actor, Actor _target, float _viewDistance)
        {
            bool isInFieldOfView = false;

            RaycastHit hit;
            Physics.Raycast(_actor.transform.position, _target.transform.position, out hit, _viewDistance);
            if (hit.collider)
            {
                Actor hitTarget = hit.collider.GetComponent<Actor>();
                if (_target == hitTarget)
                {
                    isInFieldOfView = true;
                }
            }
            return isInFieldOfView;
        }
    }
}
