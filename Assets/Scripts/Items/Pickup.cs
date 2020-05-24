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
                OnPickUpPickedEvetArgs args = new OnPickUpPickedEvetArgs()
                {
                    picker = hitActor,
                    pickup = this
                };

                EventController.QueueEvent(PickUpEvents.PICKUP_PICKED, args);

                gameObject.SetActive(false);
            }
        }
    }
}
