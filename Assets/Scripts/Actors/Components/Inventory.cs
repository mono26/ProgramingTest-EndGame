﻿using EndGame.Test.Events;
using EndGame.Test.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.Actors
{
    public class Inventory : ActorComponent
    {
        [SerializeField]
        private List<Pickup> iventoryItems = new List<Pickup>();

        public override void OnAwake(Actor _actor)
        {
            base.OnAwake(_actor);

            iventoryItems = new List<Pickup>();
        }

        private void Start()
        {
            EventController.SubscribeToEvent(PickUpEvents.PICKUP_PICKED, (args) => OnPickUpPicked((OnPickUpPicked)args));
        }

        private void OnDestroy()
        {
            EventController.UnSubscribeFromEvent(PickUpEvents.PICKUP_PICKED, (args) => OnPickUpPicked((OnPickUpPicked)args));
        }

        private void OnPickUpPicked(OnPickUpPicked _args)
        {
            if (GetOwner == _args.picker)
            {
                AddPickUpToInventory(_args.pickup);
            }
        }

        private void AddPickUpToInventory(Pickup _pickup)
        {
            iventoryItems.Add(_pickup);
        }

        public bool HasKeyItem(string _id)
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