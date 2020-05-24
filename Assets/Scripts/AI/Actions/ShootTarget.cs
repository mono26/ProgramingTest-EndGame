using EndGame.Test.Actors;
using EndGame.Test.Events;
using UnityEngine;

namespace EndGame.Test.AI
{
    [CreateAssetMenu(menuName = "PluggableAI/Actions/ShootTarget")]
    public class ShootTarget : AIAction
    {
        public override void DoAction(AIView _controller)
        {
            DoShoot(_controller);
        }

        private void DoShoot(AIView _controller)
        {
            if (!_controller.GetAIData.GetNavigationComponent.isStopped)
            {
                _controller.GetAIData.GetNavigationComponent.isStopped = true;
            }

            ShootData data = _controller.GetStateData<ShootData>();
            Vector3 startPosition = _controller.GetOwner.GetCenterOfBodyPosition;
            Vector3 targetPosition = data.GetCurrentTarget.transform.position;
            Vector3 vectorToTarget = targetPosition - startPosition;

            OnActorCommandReceiveEventArgs aimArgs = new OnActorCommandReceiveEventArgs()
            {
                actor = _controller.GetOwner,
                command = ActorCommands.Aim,
                // Means the shoot button is pressed.
                value = vectorToTarget.normalized
            };

            EventController.QueueEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, aimArgs);

            OnActorCommandReceiveEventArgs shootArgs = new OnActorCommandReceiveEventArgs()
            {
                actor = _controller.GetOwner,
                command = ActorCommands.Shoot,
                // Means the shoot button is pressed.
                value = 1.0f
            };

            EventController.QueueEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, shootArgs);
        }
    }
}
