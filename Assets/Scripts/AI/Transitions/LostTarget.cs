using UnityEngine;

namespace EndGame.Test.AI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/LostTarget")]
    public class LostTarget : TargetInSight
    {
        public override bool Decide(AIView _controller)
        {
            bool lostTarget = false;

            // If the target is not in sight check if the ai reached the chase time limit.
            if (!base.Decide(_controller))
            {
                ChaseData data = _controller.GetStateData<ChaseData>();

                lostTarget = HasLostTarget(data);
            }

            return lostTarget;
        }

        private bool HasLostTarget(ChaseData _data) => _data.GetCurrentChaseTime >= _data.GetMaxChaseTime;
    }
}
