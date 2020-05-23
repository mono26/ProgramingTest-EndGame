using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.Actors
{
    public class Detector : ActorComponent
    {
        [SerializeField]
        protected float viewDistance = 6.0f;
        [SerializeField]
        protected SphereCollider detectorTrigger = null;

        [SerializeField]
        protected Actor currentTarget = null;
        // Srialize for editor inspect.
        [SerializeField]
        protected List<Actor> nearTargets = new List<Actor>();

        public float GetViewDistance { get => viewDistance; }
        public List<Actor> GetNearTargets { get => nearTargets; }
        public virtual Actor GetCurrenTarget { get => currentTarget; }
        public virtual Vector3 GetTargetDirection { get; }

        public override void OnAwake(Actor _owner)
        {
            base.OnAwake(_owner);

            detectorTrigger.radius = viewDistance;
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
    }
}
