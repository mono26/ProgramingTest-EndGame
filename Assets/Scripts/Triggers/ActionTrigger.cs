using EndGame.Test.Events;
using EndGame.Test.Triggers;
using UnityEngine;

namespace EndGame.Test.Triggers
{
    public class ActionTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            Actor hitActor = other.GetComponent<Actor>();
            if (hitActor)
            {
                OnActorEnter(hitActor);
            }
        }

        protected virtual void OnActorEnter(Actor _actor)
        {
            OnTriggerEntered args = new OnTriggerEntered()
            {
                actor = _actor,
                trigger = this
            };

            EventController.QueueEvent(ActionTriggerEvents.TRIGGER_ENTERED, args);
        }

        private void OnTriggerExit(Collider other)
        {
            Actor hitActor = other.GetComponent<Actor>();
            if (hitActor)
            {
                OnActorExit(hitActor);
            }
        }

        protected virtual void OnActorExit(Actor _actor)
        {
            OnTriggerExited args = new OnTriggerExited()
            {
                actor = _actor,
                trigger = this
            };

            EventController.QueueEvent(ActionTriggerEvents.TRIGGER_EXITED, args);
        }
    }
}
