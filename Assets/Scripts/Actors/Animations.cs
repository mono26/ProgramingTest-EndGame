using EndGame.Test.Actors;
using EndGame.Test.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test
{
    public class Animations : ActorComponent
    {
        [SerializeField]
        private Movement movementComponent = null;
        [SerializeField]
        private Animator animationController = null;

        private const string IS_MOVING_PARAMETER = "IsMoving";
        private const string IS_SHOOTING_PARAMETER = "IsShootingBool";

        private void Awake()
        {
            movementComponent = GetComponent<Movement>();
        }

        private void Start()
        {
            // TODO remove this events.
            movementComponent.OnMovement += OnActorMovement;
            movementComponent.OnStandingStill += OnActorStandingStill;

            EventController.SubscribeToEvent(ActorEvents.ACTOR_TRIGGER_PULLED, (args) => OnActorPullingTrigger((OnActorPulledTrigger)args));
            EventController.SubscribeToEvent(ActorEvents.ACTOR_TRIGGER_RELEASED, (args) => OnActorReleasedTrigger((OnActorReleasedTrigger)args));
        }

        private void OnDestroy()
        {
            movementComponent.OnMovement -= OnActorMovement;
            movementComponent.OnStandingStill -= OnActorStandingStill;

            EventController.UnSubscribeFromEvent(ActorEvents.ACTOR_TRIGGER_PULLED, (args) => OnActorPullingTrigger((OnActorPulledTrigger)args));
            EventController.UnSubscribeFromEvent(ActorEvents.ACTOR_TRIGGER_RELEASED, (args) => OnActorReleasedTrigger((OnActorReleasedTrigger)args));
        }

        private void OnActorMovement()
        {
            if (!animationController.GetBool(IS_MOVING_PARAMETER))
            {
                animationController.SetBool(IS_MOVING_PARAMETER, true);
            }
        }

        private void OnActorStandingStill()
        {
            if (animationController.GetBool(IS_MOVING_PARAMETER))
            {
                animationController.SetBool(IS_MOVING_PARAMETER, false);
            }
        }

        private void OnActorPullingTrigger(OnActorPulledTrigger _args)
        {
            if (GetOwner == _args.actor)
            {
                animationController.SetBool(IS_SHOOTING_PARAMETER, true);
            }
        }

        private void OnActorReleasedTrigger(OnActorReleasedTrigger _args)
        {
            if (GetOwner == _args.actor)
            {
                animationController.SetBool(IS_SHOOTING_PARAMETER, false);
            }
        }
    }
}
