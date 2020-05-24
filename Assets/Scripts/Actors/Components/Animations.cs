using EndGame.Test.Actors;
using EndGame.Test.Events;
using UnityEngine;

namespace EndGame.Test
{
    public class Animations : ActorComponent
    {
        [SerializeField]
        private Animator animationController = null;

        private const string IS_MOVING_PARAMETER = "IsMoving";
        private const string IS_SHOOTING_PARAMETER = "IsShootingBool";

        public override void OnAwake(Actor _actor)
        {
            base.OnAwake(_actor);
        }

        private void Start()
        {
            EventController.SubscribeToEvent(ActorEvents.ACTOR_MOVEMENT, (args) => OnActorMovement((OnActorMovement)args));
            EventController.SubscribeToEvent(ActorEvents.ACTOR_MOVEMENT_STOPPED, (args) => OnActorStoppedMovement((OnActorStoppedMovement)args));
            EventController.SubscribeToEvent(ActorEvents.ACTOR_TRIGGER_PULLED, (args) => OnActorPullingTrigger((OnActorPulledTrigger)args));
            EventController.SubscribeToEvent(ActorEvents.ACTOR_TRIGGER_RELEASED, (args) => OnActorReleasedTrigger((OnActorReleasedTrigger)args));
        }

        private void OnDestroy()
        {
            EventController.UnSubscribeFromEvent(ActorEvents.ACTOR_MOVEMENT, (args) => OnActorMovement((OnActorMovement)args));
            EventController.UnSubscribeFromEvent(ActorEvents.ACTOR_MOVEMENT_STOPPED, (args) => OnActorStoppedMovement((OnActorStoppedMovement)args));
            EventController.UnSubscribeFromEvent(ActorEvents.ACTOR_TRIGGER_PULLED, (args) => OnActorPullingTrigger((OnActorPulledTrigger)args));
            EventController.UnSubscribeFromEvent(ActorEvents.ACTOR_TRIGGER_RELEASED, (args) => OnActorReleasedTrigger((OnActorReleasedTrigger)args));
        }

        private void OnActorMovement(OnActorMovement _args)
        {
            if (GetOwner == _args.actor)
            {
                if (!animationController.GetBool(IS_MOVING_PARAMETER))
                {
                    animationController.SetBool(IS_MOVING_PARAMETER, true);
                }
            }
        }

        private void OnActorStoppedMovement(OnActorStoppedMovement _args)
        {
            if (GetOwner == _args.actor)
            {
                if (animationController.GetBool(IS_MOVING_PARAMETER))
                {
                    animationController.SetBool(IS_MOVING_PARAMETER, false);
                }
            }
        }

        private void OnActorPullingTrigger(OnActorPulledTrigger _args)
        {
            if (GetOwner == _args.actor)
            {
                if (!animationController.GetBool(IS_SHOOTING_PARAMETER))
                {
                    animationController.SetBool(IS_SHOOTING_PARAMETER, true);
                }
            }
        }

        private void OnActorReleasedTrigger(OnActorReleasedTrigger _args)
        {
            if (GetOwner == _args.actor)
            {
                if (animationController.GetBool(IS_SHOOTING_PARAMETER))
                {
                    animationController.SetBool(IS_SHOOTING_PARAMETER, false);
                }
            }
        }
    }
}
