using EndGame.Test.Events;
using System;
using UnityEngine;

namespace EndGame.Test.Actors
{
    public class Drop : ActorComponent
    {
        private Action<IEventArgs> OnActorDeathEvent;

        [SerializeField]
        private GameObject dropObject;

        private bool canDrop = true;

        public GameObject SetDropObject { set => dropObject = value; }

        private void Start()
        {
            OnActorDeathEvent = (args) => OnActorDeath((OnActorDeath)args);

            EventController.SubscribeToEvent(ActorEvents.ACTOR_DEATH, OnActorDeathEvent);
        }

        private void OnDestroy()
        {
            OnActorDeathEvent = (args) => OnActorDeath((OnActorDeath)args);

            EventController.SubscribeToEvent(ActorEvents.ACTOR_DEATH, OnActorDeathEvent);
        }

        private void OnActorDeath(OnActorDeath _args)
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

        private void CreateDrop()
        {
            if (dropObject)
            {
                // Instantiate the drop.
                dropObject.SetActive(true);
                dropObject.transform.position = GetOwner.transform.position;
                dropObject.transform.rotation = GetOwner.transform.rotation;

                // Instantiate(dropObject, GetOwner.transform.position, GetOwner.transform.rotation);
            }
        }
    }
}
