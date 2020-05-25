using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.Actors
{
    public class Detector : ActorComponent
    {
        /// <summary>
        /// Distance of view. Associated to the trigger radius.
        /// </summary>
        [SerializeField]
        protected float viewDistance = 6.0f;
        /// <summary>
        /// Trigger detector.
        /// </summary>
        [SerializeField]
        protected SphereCollider detectorTrigger = null;

        protected List<Actor> nearTargets = new List<Actor>();

        /// <summary>
        /// Gets a reference to the near actor list.
        /// </summary>
        public List<Actor> GetNearTargets { get => nearTargets; }
        /// <summary>
        /// Gets the direction to the current target.
        /// </summary>
        public virtual Vector3 GetTargetDirection { get; }

        private void Start()
        {
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
