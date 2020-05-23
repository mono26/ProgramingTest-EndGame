using UnityEngine;

namespace EndGame.Test.AI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/LostTarget")]
    public class LostTarget : TargetInSight
    {
        public override bool Decide(AIStateController _controller, AIStateData _data)
        {
            bool lostTarget = false;
            if (!base.Decide(_controller, _data))
            {
                lostTarget = HasLostTarget((ChaseData)_data);
            }

            return lostTarget;
        }

        private bool HasLostTarget(ChaseData _data) => _data.GetCurrentChaseTime >= _data.GetMaxChaseTime;
    }
}
