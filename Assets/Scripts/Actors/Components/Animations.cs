using EndGame.Test.Actors;
using EndGame.Test.Events;
using System;
using UnityEngine;

namespace EndGame.Test
{
    public class Animations : ActorComponent
    {
        private Action<IEventArgs> OnActorMovementListener;
        private Action<IEventArgs> OnActorStoppedMovementListener;
        private Action<IEventArgs> OnActorPullingTriggerListener;
        private Action<IEventArgs> OnActorReleasedTriggerListener;

        [SerializeField]
        private Animator animationController = null;

        private const string IS_MOVING_PARAMETER = "IsMoving";
        private const string IS_SHOOTING_PARAMETER = "IsShootingBool";

        private void Start()
        {
            OnActorMovementListener = (args) => OnActorMovement((OnActorMovement)args);
            OnActorStoppedMovementListener = (args) => OnActorStoppedMovement((OnActorEventEventArgs)args);
            OnActorPullingTriggerListener = (args) => OnActorPullingTrigger((OnActorEventEventArgs)args);
            OnActorReleasedTriggerListener = (args) => OnActorReleasedTrigger((OnActorEventEventArgs)args);

            EventController.SubscribeToEvent(ActorEvents.ACTOR_MOVEMENT, OnActorMovementListener);
            EventController.SubscribeToEvent(ActorEvents.ACTOR_MOVEMENT_STOPPED, OnActorStoppedMovementListener);
            EventController.SubscribeToEvent(ActorEvents.ACTOR_TRIGGER_PULLED, OnActorPullingTriggerListener);
            EventController.SubscribeToEvent(ActorEvents.ACTOR_TRIGGER_RELEASED, OnActorReleasedTriggerListener);
        }

        private void OnDestroy()
        {
            EventController.UnSubscribeFromEvent(ActorEvents.ACTOR_MOVEMENT, OnActorMovementListener);
            EventController.UnSubscribeFromEvent(ActorEvents.ACTOR_MOVEMENT_STOPPED, OnActorStoppedMovementListener);
            EventController.UnSubscribeFromEvent(ActorEvents.ACTOR_TRIGGER_PULLED, OnActorPullingTriggerListener);
            EventController.UnSubscribeFromEvent(ActorEvents.ACTOR_TRIGGER_RELEASED, OnActorReleasedTriggerListener);
        }

        /// <summary>
        /// Tells the animator controller to play the moving animation.
        /// </summary>
        /// <param name="_args">Actor movement args.</param>
        private void OnActorMovement(OnActorMovement _args)
        {
            if (GetOwner == _args.baseArgs.actor)
            {
                if (!animationController.GetBool(IS_MOVING_PARAMETER))
                {
                    animationController.SetBool(IS_MOVING_PARAMETER, true);
                }
            }
        }

        /// <summary>
        /// Tells the animator to stop playing the movement animation.
        /// </summary>
        /// <param name="_args">Actor stoped args.</param>
        private void OnActorStoppedMovement(OnActorEventEventArgs _args)
        {
            if (GetOwner == _args.actor)
            {
                if (animationController.GetBool(IS_MOVING_PARAMETER))
                {
                    animationController.SetBool(IS_MOVING_PARAMETER, false);
                }
            }
        }

        /// <summary>
        /// Tells the animator to play the shooting animation.
        /// </summary>
        /// <param name="_args">Actor pulling trigger args.</param>
        private void OnActorPullingTrigger(OnActorEventEventArgs _args)
        {
            if (GetOwner == _args.actor)
            {
                if (!animationController.GetBool(IS_SHOOTING_PARAMETER))
                {
                    animationController.SetBool(IS_SHOOTING_PARAMETER, true);
                }
            }
        }

        /// <summary>
        /// Tells the actor to stop playing the shooting animation.
        /// </summary>
        /// <param name="_args"></param>
        private void OnActorReleasedTrigger(OnActorEventEventArgs _args)
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
