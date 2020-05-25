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

        public override bool Decide(AIView _controller)
        {
            return HasTargetInSight(_controller);
        }

        /// <summary>
        /// Check if there is any target in sight of the ai actor.
        /// </summary>
        /// <param name="_controller">Ai actor that is looking.</param>
        /// <returns></returns>
        private bool HasTargetInSight(AIView _controller)
        {
            AIDetector _detector = _controller.GetAIData.GetDetectorComponent;
            bool hasTarget = false;
            if (_detector.GetNearTargets != null && _detector.GetNearTargets.Count > 0)
            {
                foreach (Actor target in _detector.GetNearTargets)
                {
                    if (IsPotentialTargetInFieldOfView(_controller.GetOwner, target, _controller.GetAIData.GetViewAngle))
                    {
                        hasTarget = IsPotentialTargetInSight(_controller.GetOwner, target, _controller.GetAIData.GetViewRange);
                        break;
                    }
                }
            }

            return hasTarget;
        }

        /// <summary>
        /// Check if the ai is between the field of view.
        /// </summary>
        /// <param name="_actor">AI Actor that is watching.</param>
        /// <param name="_target">Posible target.</param>
        /// <param name="_viewAngle">AI actor view angle.</param>
        /// <returns></returns>
        private bool IsPotentialTargetInFieldOfView(Actor _actor, Actor _target, float _viewAngle)
        {
            bool isInFieldOfView = false;

            Vector3 directionToTarget = _target.transform.position - _actor.transform.position;
            float dotProduct = Vector3.Dot(_actor.transform.forward, directionToTarget.normalized);
            // If the dot product is less is becaus it is outside the view angle. Greater values mean inside field of view.
            if (dotProduct >= Mathf.Cos(_viewAngle / 2))
            {
                isInFieldOfView = true;
            }
            return isInFieldOfView;
        }

        /// <summary>
        /// Check if there is a ray cast that conects the ai actor and the target. With no obstacle in between.
        /// </summary>
        /// <param name="_actor">AI Actor that is watching.</param>
        /// <param name="_target">Posible target.</param>
        /// <param name="_viewAngle">AI actor view distance.</param>
        /// <returns></returns>
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

                EventController.PushEvent(DecisionEvents.TARGET_IN_SIGHT, args);
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
