using UnityEngine;

namespace EndGame.Test.AI
{
    [CreateAssetMenu(menuName = "PluggableAI/Decisions/LostTarget")]
    public class LostTarget : TargetInSight
    {
        public override bool Decide(AIStateController _controller)
        {
            bool lostTarget = false;
            if (!base.Decide(_controller))
            {
                ChaseData data = _controller.GetStateData<ChaseData>();

                lostTarget = HasLostTarget(data);

                // TODO trigger lost target event.
            }

            return lostTarget;
        }

        private bool HasLostTarget(ChaseData _data) => _data.GetCurrentChaseTime >= _data.GetMaxChaseTime;
    }
}
