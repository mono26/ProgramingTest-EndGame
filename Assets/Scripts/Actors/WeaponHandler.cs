using EndGame.Test.Actors;
using EndGame.Test.Events;
using System;
using UnityEngine;

public class WeaponHandler : ActorComponent
{
    public event Action OnPullTrigger;
    public event Action OnReleaseTrigger;

    [SerializeField]
    private WeaponFire weaponToShoot;
    [SerializeField]
    private TargetDetector targeter;

    protected void Start()
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
            actor = GetOwner,
            aimDirection = targeter.GetCurrentTargetDirection
        };

        EventController.PushEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, args);

        OnPullTrigger?.Invoke();
    }

    private void ReleaseTrigger()
    {
        OnReleaseTrigger?.Invoke();
    }
}
