using System;

namespace EndGame.Test.AI
{
    [Serializable]
    public class Transition
    {
        public Decision decision;
        public AIState trueState;
        public AIState falseState;
    }
}
