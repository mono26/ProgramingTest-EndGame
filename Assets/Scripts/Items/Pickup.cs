using EndGame.Test.Events;
using UnityEngine;

namespace EndGame.Test.Items
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField]
        private string pickUpId;

        public string GetPickupId { get => pickUpId; }

        private void OnTriggerEnter(Collider other)
        {
            Actor hitActor = other.GetComponent<Actor>();
            if (hitActor)
            {
                OnPickUpPicked args = new OnPickUpPicked()
                {
                    picker = hitActor,
                    pickup = this
                };

                EventController.PushEvent(PickUpEvents.PICKUP_PICKED, args);

                gameObject.SetActive(false);
            }
        }
    }
}
