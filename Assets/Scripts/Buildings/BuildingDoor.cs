using EndGame.Test.Actors;
using EndGame.Test.Events;
using EndGame.Test.Game;
using EndGame.Test.Triggers;
using UnityEngine;

public class BuildingDoor : MonoBehaviour
{
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
        EventController.SubscribeToEvent(ActionTriggerEvents.TRIGGER_ENTERED, (args) => OnTriggerEntered((OnTriggerEntered)args));
        EventController.SubscribeToEvent(ActionTriggerEvents.TRIGGER_EXITED, (args) => OnTriggerExited((OnTriggerExited)args));
    }

    private void OnDestroy()
    {
        EventController.UnSubscribeFromEvent(ActionTriggerEvents.TRIGGER_ENTERED, (args) => OnTriggerEntered((OnTriggerEntered)args));
        EventController.UnSubscribeFromEvent(ActionTriggerEvents.TRIGGER_EXITED, (args) => OnTriggerExited((OnTriggerExited)args));
    }

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
