using EndGame.Test.Events;
using EndGame.Test.Triggers;
using UnityEngine;

public class ActionTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Actor hitActor = other.GetComponent<Actor>();
        if (hitActor)
        {

            OnTriggerEntered args = new OnTriggerEntered()
            {
                actor = hitActor,
                trigger = this
            };

            EventController.PushEvent(ActionTriggerEvents.TRIGGER_ENTERED, args);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Actor hitActor = other.GetComponent<Actor>();
        if (hitActor)
        {

            OnTriggerExited args = new OnTriggerExited()
            {
                actor = hitActor,
                trigger = this
            };

            EventController.PushEvent(ActionTriggerEvents.TRIGGER_EXITED, args);
        }
    }
}
