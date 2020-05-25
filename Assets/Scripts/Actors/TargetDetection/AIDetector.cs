using EndGame.Test.AI;
using EndGame.Test.Events;
using EndGame.Test.Events.AI;
using UnityEngine;

namespace EndGame.Test.Actors
{
    public class AIDetector : Detector
    {
        private Actor currentTarget = null;

        public override Vector3 GetTargetDirection { get => currentTarget.transform.position - GetOwner.transform.position; }

        private void Start()
        {
            viewDistance = GetComponent<AIData>().GetViewRange;
            detectorTrigger.radius = viewDistance;

            EventController.SubscribeToEvent(DecisionEvents.TARGET_IN_SIGHT, (args) => OnTargetInSight((OnTargetInSightEventArgs)args));
        }

        /// <summary>
        /// Stores a reference of the target in sight.
        /// </summary>
        /// <param name="_args">Target in sight args.</param>
        private void OnTargetInSight(OnTargetInSightEventArgs _args)
        {
            if (_args.actor == GetOwner)
            {
                currentTarget = _args.target;
            }
        }
    }
}
