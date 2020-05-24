﻿using EndGame.Test.Actors;
using EndGame.Test.Events;
using EndGame.Test.Events.AI;
using UnityEngine;

namespace EndGame.Test.AI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/TargetInShootRange")]
    public class TargetInShootRange : TargetInSight
    {
        public override bool Decide(AIView _controller)
        {
            return IsTargetInShootRange(_controller);
        }

        /// <summary>
        /// Cast a ray in the target direction and check if its at shooting distance.
        /// </summary>
        /// <param name="_controller">AIActor reference.</param>
        /// <returns></returns>
        protected bool IsTargetInShootRange(AIView _controller)
        {
            bool inRange = false;

            ChaseData data = _controller.GetStateData<ChaseData>();

            Vector3 startPosition = _controller.GetOwner.GetCenterOfBodyPosition;
            Vector3 targetPosition = data.GetCurrentTarget.GetCenterOfBodyPosition;
            Vector3 vectorToTarget = targetPosition - startPosition;
            float viewDistance = _controller.GetAIData.GetViewRange;

            // Raycast in the target direction to see if there is no obstacle.
            Actor hitTarget = RayScan(startPosition, vectorToTarget.normalized, viewDistance, rayCastLayers);
            if (data.GetCurrentTarget == hitTarget)
            {
                Vector3 directionToHit = hitTarget.GetCenterOfBodyPosition - startPosition;
                inRange = directionToHit.sqrMagnitude <= _controller.GetAIData.GetShootRange * _controller.GetAIData.GetShootRange;
                if (inRange)
                {
                    OnTargetInShootRange args = new OnTargetInShootRange()
                    {
                        actor = data.GetOwner,
                        target = hitTarget
                    };

                    EventController.PushEvent(DecisionEvents.TARGET_IN_SHOOT_RANGE, args);
                }            
            }

            return inRange;
        }
    }
}
