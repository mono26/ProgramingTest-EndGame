using EndGame.Test.Actors;
using EndGame.Test.Events;
using EndGame.Test.Game;
using EndGame.Test.Triggers;
using System;
using UnityEngine;

namespace EndGame.Test.Buildings
{
    public class BuildingDoor : MonoBehaviour
    {
        private Action<IEventArgs> OnTriggerEnteredListener;
        private Action<IEventArgs> OnTriggerExitedListener;

        [SerializeField]
        private Animator doorController = null;
        [SerializeField]
        private ActionTrigger openCloseTrigger = null;
        [SerializeField]
        private Collider doorCollider = null;
        [SerializeField]
        private string keyId = null;

        private void Awake()
        {
            doorController.GetComponent<Animator>();
        }

        private void Start()
        {
            OnTriggerEnteredListener = (args) => OnTriggerEntered((OnTriggerEntered)args);
            OnTriggerExitedListener = (args) => OnTriggerExited((OnTriggerExited)args);

            EventController.SubscribeToEvent(ActionTriggerEvents.TRIGGER_ENTERED, OnTriggerEnteredListener);
            EventController.SubscribeToEvent(ActionTriggerEvents.TRIGGER_EXITED, OnTriggerExitedListener);
        }

        private void OnDestroy()
        {
            EventController.UnSubscribeFromEvent(ActionTriggerEvents.TRIGGER_ENTERED, OnTriggerEnteredListener);
            EventController.UnSubscribeFromEvent(ActionTriggerEvents.TRIGGER_EXITED, OnTriggerExitedListener);
        }

        /// <summary>
        /// Open the door if an actor entered the activation trigger. If the actor is the player check for the key in the inventory.
        /// </summary>
        /// <param name="_args">Trigger entered args.</param>
        private void OnTriggerEntered(OnTriggerEntered _args)
        {
            if (openCloseTrigger == _args.trigger)
            {
                // We want to always open the door to the AI.
                bool opendDoor = true;
                if (_args.actor.CompareTag("Player"))
                {
                    Inventory playerInventory = _args.actor.GetComponent<Inventory>();
                    if (!playerInventory.HasItem(keyId))
                    {
                        opendDoor = false;

                        OnPlayerHasNoKeyEventArgs args = new OnPlayerHasNoKeyEventArgs()
                        {

                        };

                        EventController.QueueEvent(GameEvents.PLAYER_HAS_NO_KEY, args);
                    }

                }

                if (opendDoor)
                {
                    OpenDoor();
                }
            }
        }

        private void OpenDoor()
        {
            doorController.SetBool("Open", true);
            doorController.SetBool("Close", false);

            doorCollider.enabled = false;
        }

        /// <summary>
        /// Closed the door when an actor leaves the trigger area.
        /// </summary>
        /// <param name="_args"></param>
        private void OnTriggerExited(OnTriggerExited _args)
        {
            if (openCloseTrigger == _args.trigger)
            {
                CloseDoor();
            }
        }

        private void CloseDoor()
        {
            doorController.SetBool("Open", false);
            doorController.SetBool("Close", true);

            doorCollider.enabled = true;
        }
    }
}
