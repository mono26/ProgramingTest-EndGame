using EndGame.Test.Events;
using UnityEngine;

namespace EndGame.Test.Items
{
    public class PickUpEvents
    {
        public const string PICKUP_PICKED = "event.pickup.picked";
    }

    public struct OnPickUpPickedEventArgs : IEventArgs
    {
        public Actor picker;
        public Pickup pickup;
    }
}
