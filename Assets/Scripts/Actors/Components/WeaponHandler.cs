using EndGame.Test.Actors;
using EndGame.Test.Events;
using System;
using UnityEngine;

public class WeaponHandler : ActorComponent
{
    [SerializeField]
    private WeaponFire weaponToShoot = null;
    [SerializeField]
    private Detector targeter = null;

    public override void OnAwake(Actor _actor)
    {
        base.OnAwake(_actor);

        targeter = GetComponent<Detector>();
    }

    private void Start()
    {
        // TODO subscribe to on target in sight.
        EventController.SubscribeToEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, (args) => OnShootCommand((OnActorCommandReceiveEventArgs)args));
        EventController.SubscribeToEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, (args) => OnShootCommand((OnActorCommandReceiveEventArgs)args));
    }

    private void OnShootCommand(OnActorCommandReceiveEventArgs _args)
    {
        if (GetOwner == _args.actor)
        {
            if (_args.command.Equals(ActorCommands.Shoot))
            {
                float inputValue = (float)_args.value;
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

    private void PullTrigger()
    {
        OnActorPulledTrigger args = new OnActorPulledTrigger()
        {
            actor = GetOwner
        };

        EventController.PushEvent(ActorEvents.ACTOR_TRIGGER_PULLED, args);
    }

    private void ReleaseTrigger()
    {
        OnActorReleasedTrigger args = new OnActorReleasedTrigger()
        {
            actor = GetOwner
        };

        EventController.PushEvent(ActorEvents.ACTOR_TRIGGER_RELEASED, args);
    }

    /// <summary>
    /// Called by an animation event at the start of the shooting animation.
    /// </summary>
    private void OnShootAnimationInit()
    {
        Vector3 targetDirection = targeter.GetTargetDirection;
        weaponToShoot.FireWeapon(targetDirection);

        OnActorFireWeapon args = new OnActorFireWeapon()
        {
            actor = GetOwner,
            aimDirection = targetDirection
        };

        EventController.PushEvent(ActorEvents.ACTOR_FIRE_WEAPON, args);
    }

    /// <summary>
    /// Called by an animation event at the end of the shooting animation.
    /// </summary>
    private void OnFinishShootAnimtionEvent() => weaponToShoot.OnFinishShootAnimtionEvent();
}
