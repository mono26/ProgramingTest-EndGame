using EndGame.Test.Events;
using UnityEngine;

namespace EndGame.Test.Triggers
{
    public class ActionTriggerEvents
    {
        public const string TRIGGER_ENTERED = "events.trigger.entered";
        public const string TRIGGER_EXITED = "events.trigger.exited";
    }

    public struct OnTriggerEntered : IEventArgs
    {
        public Actor actor;
        public ActionTrigger trigger;
    }

    public struct OnTriggerExited : IEventArgs
    {
        public Actor actor;
        public ActionTrigger trigger;
    }
}
