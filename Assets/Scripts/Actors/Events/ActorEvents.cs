using EndGame.Test.Events;
using UnityEngine;

namespace EndGame.Test.Actors
{
    public class ActorEvents : MonoBehaviour
    {
        public const string ACTOR_COMMAND_RECEIVE = "event.actor.command.receive";
        public const string ACTOR_MOVEMENT = "event.actor.movement";
        public const string ACTOR_MOVEMENT_STOPPED = "event.actor.movement.stoped";
        public const string ACTOR_TRIGGER_PULLED = "event.actor.trigger.pulled";
        public const string ACTOR_FIRE_WEAPON = "event.actor.weapon.fire";
        public const string ACTOR_TRIGGER_RELEASED = "event.actor.trigger.released";
        public const string ACTOR_HIT_BY_BULLET = "event.actor.bullet.hit";
    }

    public struct OnActorCommandReceiveEventArgs : IEventArgs
    {
        public Actor actor;
        public ActorCommands command;
        public object value;
    }

    // TODO create BaseEventArgs with actor as base.
    public struct OnActorMovement : IEventArgs
    {
        public Actor actor;
        public Vector3 direction;
    }

    public struct OnActorStoppedMovement : IEventArgs
    {
        public Actor actor;
    }

    public struct OnActorPulledTrigger : IEventArgs
    {
        public Actor actor;
    }

    public struct OnActorFireWeapon : IEventArgs
    {
        public Actor actor;
        public Vector3 aimDirection;
    }

    public struct OnActorReleasedTrigger : IEventArgs
    {
        public Actor actor;
    }

    public struct OnBulletHitActor : IEventArgs
    {
        public Actor actor;
    }
}
