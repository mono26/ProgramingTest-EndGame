using EndGame.Test.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.Actors
{
    public class ActorEvents : MonoBehaviour
    {
        public const string ACTOR_COMMAND_RECEIVE = "event.actor.command.receive";
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

    }

    public struct OnActorStoppedMovement : IEventArgs
    {

    }
}
