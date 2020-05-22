﻿using EndGame.Test.Actors;
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
        private LayerMask rayCastLayers;

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
                if (IsPotentialTargetInFieldOfView(_actor, target, _targeter.GetViewAngle))
                {
                    hasTarget = IsPotentialTargetInSight(_actor, target, _targeter.GetViewDistance);
                    break;
                }
            }

            return hasTarget;
        }

        private bool IsPotentialTargetInFieldOfView(Actor _actor, Actor _target, float _viewAngle)
        {
            bool isInFieldOfView = false;

            // Debug.DrawLine(_actor.transform.position, _target.transform.position, Color.cyan, 3.0f);

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

            RaycastHit hit = PhysicsHelper.CastRayForHits(startPosition, directionToTarget.normalized, _viewDistance, rayCastLayers);

            if (hit.collider)
            {
                Actor hitTarget = hit.collider.GetComponent<Actor>();
                if (_target == hitTarget)
                {
                    inSight = true;

                    //Debug.DrawLine(startPosition, targetPosition, Color.green, 3.0f);

                    OnTargetInSightEventArgs args = new OnTargetInSightEventArgs()
                    {
                        actor = _actor,
                        target = hitTarget
                    };

                    EventController.PushEventImmediately(DecisionEvents.TARGET_IN_SIGHT, args);
                }
            }

            //// Debug.DrawLine(startPosition, targetPosition, Color.red, 3.0f);
            //if (Physics.Raycast(startPosition, directionToTarget.normalized, out hit, _viewDistance, rayCastLayers))
            //{
            //    if (hit.collider)
            //    {
            //        Actor hitTarget = hit.collider.GetComponent<Actor>();
            //        if (_target == hitTarget)
            //        {
            //            inSight = true;

            //            //Debug.DrawLine(startPosition, targetPosition, Color.green, 3.0f);

            //            OnTargetInSightEventArgs args = new OnTargetInSightEventArgs()
            //            {
            //                actor = _actor,
            //                target = hitTarget
            //            };

            //            EventController.PushEventImmediately(DecisionEvents.TARGET_IN_SIGHT, args);
            //        }
            //    }
            //}
            return inSight;
        }
    }
}
