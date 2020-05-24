using UnityEngine;

namespace EndGame.Test.AI
{
    public class AIData : MonoBehaviour
    {
        [SerializeField]
        public float viewAngle;
        [SerializeField]
        public float viewRange;
        [SerializeField]
        public float shootRange;

        public float GetViewAngle { get => viewAngle; }
        public float GetViewRange { get => viewRange; }
        public float GetShootRange { get => shootRange; }
    }
}
