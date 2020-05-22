using EndGame.Test.Events;
using UnityEngine;

namespace EndGame.Test.Actors
{
    public class PlayerInput : ActorComponent
    {
        private void Update()
        {
            // Get the horizontal and vertical input.
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            // Only calculate the target position if there's movement input.
            if (!horizontalInput.Equals(0.0f) || !verticalInput.Equals(0.0f))
            {
                // Vertical input is mapped out to the z axis.
                Vector3 inputVector = new Vector3(horizontalInput, 0, verticalInput);

                var args = new OnActorCommandReceiveEventArgs()
                {
                    actor = GetOwner,
                    command = ActorCommands.Move,
                    value = inputVector
                };

                EventController.PushEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, args);
            }
        }
    }
}
