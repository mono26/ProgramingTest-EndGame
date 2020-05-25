using EndGame.Test.Events;
using EndGame.Test.Weapons;
using System;
using UnityEngine;

namespace EndGame.Test.Actors
{
    public class WeaponHandler : ActorComponent
    {
        private Action<IEventArgs> OnActorCommandReceiveListener;

        [SerializeField]
        private WeaponFire weaponToShoot = null;

        private void Start()
        {
            OnActorCommandReceiveListener = (args) => OnShootCommand((OnActorCommandReceiveEventArgs)args);

            EventController.SubscribeToEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, OnActorCommandReceiveListener);
        }

        private void OnDestroy()
        {
            EventController.UnSubscribeFromEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, OnActorCommandReceiveListener);
        }

        /// <summary>
        /// Called when the actor receibes a shoot command.
        /// </summary>
        /// <param name="_args"></param>
        private void OnShootCommand(OnActorCommandReceiveEventArgs _args)
        {
            if (GetOwner == _args.baseArgs.actor)
            {
                if (_args.command.Equals(ActorCommands.Shoot))
                {
                    float inputValue = (float)_args.value;
                    // If the value is grater than 0.0f it means the button is pressed.
                    if (inputValue > 0.0f)
                    {
                        PullTrigger();
                    }
                    else
                    {
                        ReleaseTrigger();
                    }
                }
            }
        }

        /// <summary>
        /// Fires a pull trigger event.
        /// </summary>
        private void PullTrigger()
        {
            OnActorEventEventArgs args = new OnActorEventEventArgs()
            {
                actor = GetOwner
            };

            EventController.QueueEvent(ActorEvents.ACTOR_TRIGGER_PULLED, args);
        }

        /// <summary>
        /// Fires a release trigger event.
        /// </summary>
        private void ReleaseTrigger()
        {
            OnActorEventEventArgs args = new OnActorEventEventArgs()
            {
                actor = GetOwner
            };

            EventController.QueueEvent(ActorEvents.ACTOR_TRIGGER_RELEASED, args);
        }

        /// <summary>
        /// Called by an animation event at the start of the shooting animation.
        /// </summary>
        private void OnShootAnimationInit()
        {
            weaponToShoot.FireWeapon();
        }

        /// <summary>
        /// Called by an animation event at the end of the shooting animation.
        /// </summary>
        private void OnShootAnimationFinish() => weaponToShoot.OnFinishShootAnimtionEvent();
    }

}