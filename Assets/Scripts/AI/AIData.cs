using EndGame.Test.Actors;
using UnityEngine;
using UnityEngine.AI;

namespace EndGame.Test.AI
{
    public class AIData : ActorComponent
    {
        [SerializeField]
        public float viewAngle;
        [SerializeField]
        public float viewRange;
        [SerializeField]
        public float shootRange;
        [SerializeField]
        private NavMeshAgent navigationComponent = null;
        [SerializeField]
        private AIDetector detectorComponent = null;

        private void Start()
        {
            navigationComponent = GetComponent<NavMeshAgent>();
            detectorComponent = GetComponent<AIDetector>();
        }

        public float GetViewAngle { get => viewAngle; }
        public float GetViewRange { get => viewRange; }
        public float GetShootRange { get => shootRange; }
        public NavMeshAgent GetNavigationComponent { get => navigationComponent; }
        public AIDetector GetDetectorComponent { get => detectorComponent; }
    }
}
