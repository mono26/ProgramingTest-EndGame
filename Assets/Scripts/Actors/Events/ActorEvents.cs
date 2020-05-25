using EndGame.Test.Events;
using UnityEngine;

namespace EndGame.Test.Actors
{
    public class ActorEvents
    {
        public const string ACTOR_COMMAND_RECEIVE = "event.actor.command.receive";
        public const string ACTOR_MOVEMENT = "event.actor.movement";
        public const string ACTOR_MOVEMENT_STOPPED = "event.actor.movement.stoped";
        public const string ACTOR_TRIGGER_PULLED = "event.actor.trigger.pulled";
        public const string ACTOR_FIRE_WEAPON = "event.actor.weapon.fire";
        public const string ACTOR_TRIGGER_RELEASED = "event.actor.trigger.released";
        public const string ACTOR_HIT_BY_BULLET = "event.actor.bullet.hit";
        public const string ACTOR_HEALTH_UPDATED = "event.actor.health.updated";
        public const string ACTOR_DEATH = "event.actor.death";
        public const string ACTOR_RESPAWN = "event.actor.respawn";
    }

    /// <summary>
    /// Base args of an actor related event.
    /// </summary>
    public struct OnActorEventEventArgs : IEventArgs
    {
        public Actor actor;
    }

    /// <summary>
    /// Args of an actor command event.
    /// </summary>
    public struct OnActorCommandReceiveEventArgs : IEventArgs
    {
        public OnActorEventEventArgs baseArgs;
        public ActorCommands command;
        public object value;
    }

    /// <summary>
    /// Args of an actor movement event.
    /// </summary>
    public struct OnActorMovement : IEventArgs
    {
        public OnActorEventEventArgs baseArgs;
        public Vector3 movementDirection;
    }

    /// <summary>
    /// Args of an actor fires weapon event.
    /// </summary>
    public struct OnActorFireWeapon : IEventArgs
    {
        public OnActorEventEventArgs baseArgs;
        public Vector3 aimDirection;
    }

    /// <summary>
    /// Args containing information when an actor updates it's health value.
    /// </summary>
    public struct OnActorHealthUpdated : IEventArgs
    {
        public OnActorEventEventArgs baseArgs;
        public Health healthComponent;
    }
}
