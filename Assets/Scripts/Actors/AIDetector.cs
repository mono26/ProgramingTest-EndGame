using EndGame.Test.Events;
using EndGame.Test.Events.AI;
using UnityEngine;

namespace EndGame.Test.Actors
{
    public class AIDetector : Detector
    {
        [SerializeField]
        private float viewAngle = 60.0f;

        public float GetViewAngle { get => viewAngle; }
        public override Vector3 GetTargetDirection { get => currentTarget.transform.position - GetOwner.transform.position; }

        //private void OnDrawGizmos()
        //{

        //}

        public override void OnAwake(Actor _owner)
        {
            base.OnAwake(_owner);

            detectorTrigger.radius = viewDistance;
        }

        private void Start()
        {
            EventController.SubscribeToEvent(DecisionEvents.TARGET_IN_SIGHT, (args) => OnTargetInSight((OnTargetInSightEventArgs)args));
        }

        private void OnTargetInSight(OnTargetInSightEventArgs _args)
        {
            if (_args.actor == GetOwner)
            {
                currentTarget = _args.target;

                Debug.DrawLine(GetOwner.transform.position, _args.target.transform.position, Color.magenta, 3.0f);
            }
        }
    }
}
