using EndGame.Test.Events;
using System;
using UnityEngine;

namespace EndGame.Test.Actors
{
    public class Drop : ActorComponent
    {
        private Action<IEventArgs> OnActorDeathEvent;

        /// <summary>
        /// Object to drop.
        /// </summary>
        [SerializeField]
        private GameObject dropObject;

        private bool canDrop = true;

        public GameObject SetDropObject { set => dropObject = value; }

        private void Start()
        {
            OnActorDeathEvent = (args) => OnActorDeath((OnActorEventEventArgs)args);

            EventController.SubscribeToEvent(ActorEvents.ACTOR_DEATH, OnActorDeathEvent);
        }

        private void OnDestroy()
        {
            EventController.UnSubscribeFromEvent(ActorEvents.ACTOR_DEATH, OnActorDeathEvent);
        }

        /// <summary>
        /// Drops and objet, if there is one.
        /// </summary>
        /// <param name="_args"></param>
        private void OnActorDeath(OnActorEventEventArgs _args)
        {
            if (canDrop)
            {
                if (GetOwner == _args.actor)
                {
                    CreateDrop();

                    // We only want to drop once.
                    canDrop = false;
                }
            }
        }

        /// <summary>
        /// Create the drop in the world.
        /// </summary>
        private void CreateDrop()
        {
            if (dropObject)
            {
                // Activate the drop object.
                dropObject.SetActive(true);
                dropObject.transform.position = GetOwner.transform.position;
                dropObject.transform.rotation = GetOwner.transform.rotation;
            }
        }
    }
}
