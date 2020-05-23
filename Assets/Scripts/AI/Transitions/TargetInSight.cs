using EndGame.Test.Actors;
using EndGame.Test.Events;
using EndGame.Test.Events.AI;
using EndGame.Test.Utils;
using UnityEngine;

namespace EndGame.Test.AI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/TargetInSight")]
    public class TargetInSight : Decision
    {
        [SerializeField]
        protected LayerMask rayCastLayers;

        // TODO check if is best to use actor instead AIStateController.
        public override bool Decide(AIView _controller)
        {
            Actor actor = _controller.GetOwner;
            AIDetector targeter = actor.GetComponent<AIDetector>();
            return HasTargetInSight(actor, targeter);
        }

        private bool HasTargetInSight(Actor _actor, AIDetector _targeter)
        {
            bool hasTarget = false;
            if (_targeter.GetNearTargets != null && _targeter.GetNearTargets.Count > 0)
            {
                foreach (Actor target in _targeter.GetNearTargets)
                {
                    if (IsPotentialTargetInFieldOfView(_actor, target, _targeter.GetViewAngle))
                    {
                        hasTarget = IsPotentialTargetInSight(_actor, target, _targeter.GetViewDistance);
                        break;
                    }
                }
            }

            return hasTarget;
        }

        private bool IsPotentialTargetInFieldOfView(Actor _actor, Actor _target, float _viewAngle)
        {
            bool isInFieldOfView = false;

            //Debug.DrawLine(_actor.transform.position, _target.transform.position, Color.cyan, 3.0f);

            Vector3 directionToTarget = _target.transform.position - _actor.transform.position;
            float dotProduct = Vector3.Dot(_actor.transform.forward, directionToTarget.normalized);
            if (dotProduct >= Mathf.Cos(_viewAngle / 2))
            {
                isInFieldOfView = true;
            }
            return isInFieldOfView;
        }

        private bool IsPotentialTargetInSight(Actor _actor, Actor _target, float _viewDistance)
        {
            bool inSight = false;

            Vector3 startPosition = _actor.GetCenterOfBodyPosition;
            Vector3 targetPosition = _target.GetCenterOfBodyPosition;
            Vector3 directionToTarget = targetPosition - startPosition;

            Actor hitTarget = RayScan(startPosition, directionToTarget.normalized, _viewDistance, rayCastLayers);
            if (_target == hitTarget)
            {
                inSight = true;

                Debug.DrawLine(startPosition, targetPosition, Color.green, 3.0f);

                OnTargetInSightEventArgs args = new OnTargetInSightEventArgs()
                {
                    actor = _actor,
                    target = hitTarget
                };

                EventController.PushEventImmediately(DecisionEvents.TARGET_IN_SIGHT, args);
            }

            return inSight;
        }

        protected Actor RayScan(Vector3 _startPosition, Vector3 _direction, float _distance, LayerMask _layers)
        {
            Actor targetInSight = null;
            RaycastHit hit = PhysicsHelper.CastRayForHits(_startPosition, _direction, _distance, _layers);
            if (hit.collider)
            {
                targetInSight = hit.collider.GetComponent<Actor>();
            }

            return targetInSight;
        }
    }
}
