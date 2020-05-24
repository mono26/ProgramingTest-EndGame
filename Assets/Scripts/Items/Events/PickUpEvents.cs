using EndGame.Test.Events;
using UnityEngine;

namespace EndGame.Test.Items
{
    public class PickUpEvents
    {
        public const string PICKUP_PICKED = "event.pickup.picked";
        public const string PICKUP_COFFEE_KEY = "event.pickup.coffee.key";
    }

    public struct OnPickUpPickedEvetArgs : IEventArgs
    {
        public Actor picker;
        public Pickup pickup;
    }

    public struct OnPickUpCoffeeKeyEventArgs : IEventArgs
    {

    }
}
