using EndGame.Test.Events;

namespace EndGame.Test.Events.AI
{
    public class DecisionEvents
    {
        public const string WAIT_FINISH = "event.decision.wait.finish";
        public const string PATROL_POINT_REACHED = "event.decision.patrol.point.reach";
        public const string TARGET_IN_SIGHT = "event.decision.target.in.sight";
    }


    public struct OnWaitFinishedEventArgs : IEventArgs
    {
        public Actor actor;
    }

    public struct OnPatrolPointReachedEventArgs : IEventArgs
    {
        public Actor actor;
    }

    public struct OnTargetInSightEventArgs : IEventArgs
    {
        public Actor actor;
        public Actor target;
    }
}
