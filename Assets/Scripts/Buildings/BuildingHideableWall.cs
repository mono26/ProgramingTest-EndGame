using EndGame.Test.Events;
using EndGame.Test.Triggers;
using UnityEngine;

public class BuildingHideableWall : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer rendererComponent;
    [SerializeField]
    private ActionTrigger hideTrigger;

    private void Awake()
    {
        rendererComponent = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        ActivateWallSprite(true);

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
        if (hideTrigger == _args.trigger)
        {
            // Only hide the sprite if the playerr enters.
            if (_args.actor.CompareTag("Player"))
            {
                OnHideTriggerEntered();
            }
        }
    }

    private void OnTriggerExited(OnTriggerExited _args)
    {
        if (hideTrigger == _args.trigger)
        {
            if (_args.actor.CompareTag("Player"))
            {
                OnHideTriggerExited();
            }
        }
    }

    private void OnHideTriggerEntered()
    {
        Debug.Log("Entered building");
        ActivateWallSprite(false);
    }

    private void ActivateWallSprite(bool _activate)
    {
        rendererComponent.enabled = _activate;
    }

    private void OnHideTriggerExited()
    {
        Debug.Log("Exited building");
        ActivateWallSprite(true);
    }
}
