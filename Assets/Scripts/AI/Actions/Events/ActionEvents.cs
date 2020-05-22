using EndGame.Test.Events;

namespace EndGame.Test.Events.AI
{
    public class ActionEvents
    {
        public const string WAITED_ACTION = "event.action.wait";
    }

    public struct OnWaitedActionEventArgs : IEventArgs
    {
        public Actor actor;
        public float waitedTime;
    }
}
