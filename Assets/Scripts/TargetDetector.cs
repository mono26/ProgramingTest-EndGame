using EndGame.Test.Actors;
using EndGame.Test.Events;
using EndGame.Test.Events.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.Actors
{
    public class TargetDetector : ActorComponent
    {
        [SerializeField]
        private float viewAngle = 60.0f;
        [SerializeField]
        private float viewDistance = 6.0f;
        [SerializeField]
        private SphereCollider detectorTrigger;

        private Actor currentTarget = null;
        // Srialize for editor inspect.
        [SerializeField]
        private List<Actor> nearTargets;

        public float GetViewAngle { get => viewAngle; }
        public float GetViewDistance { get => viewDistance; }
        public List<Actor> GetNearTargets { get => nearTargets; }

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

        private void OnTriggerEnter(Collider other)
        {
            Actor actor = other.GetComponent<Actor>();
            if (actor && GetOwner != actor)
            {
                nearTargets.Add(actor);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            Actor actor = other.GetComponent<Actor>();
            if (actor && GetOwner != actor)
            {
                nearTargets.Remove(actor);
            }
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
