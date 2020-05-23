using EndGame.Test.Events;
using UnityEngine;

namespace EndGame.Test.Actors
{
    public class Rotator : ActorComponent
    {
        protected virtual void Start()
        {
            EventController.SubscribeToEvent(ActorEvents.ACTOR_MOVEMENT, (args) => OnActorMovement((OnActorMovement)args));
            EventController.SubscribeToEvent(ActorEvents.ACTOR_FIRE_WEAPON, (args) => OnActorFireWeapon((OnActorFireWeapon)args));
        }

        protected virtual void OnDestroy()
        {
            // TODO store as delegates ??
            EventController.UnSubscribeFromEvent(ActorEvents.ACTOR_MOVEMENT, (args) => OnActorMovement((OnActorMovement)args));
            EventController.UnSubscribeFromEvent(ActorEvents.ACTOR_FIRE_WEAPON, (args) => OnActorFireWeapon((OnActorFireWeapon)args));
        }

        private void OnActorMovement(OnActorMovement _args)
        {
            if (GetOwner == _args.actor)
            {
                RotateTowardsTargetDirection(_args.direction);
            }
        }

        private void RotateTowardsTargetDirection(Vector3 _nextDirection)
        {
            GetOwner.transform.LookAt(GetOwner.transform.position + _nextDirection);
        }

        // TODO see if is beter to when is aiming or aim command.
        protected void OnActorFireWeapon(OnActorFireWeapon _args)
        {
            if (GetOwner == _args.actor)
            {
                RotateTowardsTargetDirection(_args.aimDirection);
            }
        }
    }
}
