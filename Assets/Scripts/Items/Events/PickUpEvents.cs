using EndGame.Test.Events;
using UnityEngine;

namespace EndGame.Test.Items
{
    public class PickUpEvents
    {
        public const string PICKUP_PICKED = "events.pickup.picked";
    }

    public struct OnPickUpPicked : IEventArgs
    {
        public Actor picker;
        public Pickup pickup;
    }
}
