using EndGame.Test.Events;
using EndGame.Test.Items;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.Actors
{
    public class Inventory : ActorComponent
    {
        private Action<IEventArgs> OnPickUpPickedListener = null;

        [SerializeField]
        private List<Pickup> iventoryItems = new List<Pickup>();

        private void Start()
        {
            OnPickUpPickedListener = (args) => OnPickUpPicked((OnPickUpPickedEvetArgs)args);

            EventController.SubscribeToEvent(PickUpEvents.PICKUP_PICKED, OnPickUpPickedListener);
        }

        private void OnDestroy()
        {
            EventController.UnSubscribeFromEvent(PickUpEvents.PICKUP_PICKED, OnPickUpPickedListener);
        }

        /// <summary>
        /// Adds a pickup to the actor invetory and sends a event when is the coffee key.
        /// </summary>
        /// <param name="_args"></param>
        private void OnPickUpPicked(OnPickUpPickedEvetArgs _args)
        {
            if (GetOwner == _args.picker)
            {
                AddPickUpToInventory(_args.pickup);

                if (_args.pickup.GetPickupId.Equals("CoffeeKey"))
                {
                    OnPickUpCoffeeKeyEventArgs args = new OnPickUpCoffeeKeyEventArgs
                    {

                    };

                    EventController.QueueEvent(PickUpEvents.PICKUP_COFFEE_KEY, args);
                }
            }
        }

        /// <summary>
        /// Adds the pickup to the inventory list.
        /// </summary>
        /// <param name="_pickup"></param>
        private void AddPickUpToInventory(Pickup _pickup)
        {
            iventoryItems.Add(_pickup);
        }

        /// <summary>
        /// Checks if the actor has an item with a specific id.
        /// </summary>
        /// <param name="_id">Id to look.</param>
        /// <returns></returns>
        public bool HasItem(string _id)
        {
            bool hasItem = false;
            foreach (Pickup item in iventoryItems)
            {
                if (item.GetPickupId == _id)
                {
                    hasItem = true;
                }
            }

            return hasItem;
        }
    }
}
