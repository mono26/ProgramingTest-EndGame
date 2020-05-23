using EndGame.Test.Events;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace EndGame.Test.Actors
{
    public class PlayerView : ActorComponent
    {
        private Action<IEventArgs> OnActorHealthUpdated;

        [SerializeField]
        private Image healthBarFillComponent = null;

        private void Start()
        {
            OnActorHealthUpdated = (args) => UpdateHealthBar((OnActorHealthUpdated)args);

            EventController.SubscribeToEvent(ActorEvents.ACTOR_HEALTH_UPDATED, OnActorHealthUpdated);
            EventController.SubscribeToEvent(ActorEvents.ACTOR_DEATH, (args) => OnActorDeath((OnActorDeath)args));
        }

        private void OnDestroy()
        {
            EventController.UnSubscribeFromEvent(ActorEvents.ACTOR_HEALTH_UPDATED, OnActorHealthUpdated);
        }

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

                OnActorCommandReceiveEventArgs args = new OnActorCommandReceiveEventArgs()
                {
                    actor = GetOwner,
                    command = ActorCommands.Move,
                    value = inputVector
                };

                // TODO Pack commands into one.
                EventController.PushEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, args);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                OnActorCommandReceiveEventArgs args = new OnActorCommandReceiveEventArgs()
                {
                    actor = GetOwner,
                    command = ActorCommands.Shoot,
                    // Means the shoot button is pressed.
                    value = 1.0f
                };

                EventController.PushEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, args);
            }
            if (Input.GetButtonUp("Fire1"))
            {
                OnActorCommandReceiveEventArgs args = new OnActorCommandReceiveEventArgs()
                {
                    actor = GetOwner,
                    command = ActorCommands.Shoot,
                    // Means the shoot button has been released.
                    value = 0.0f
                };

                EventController.PushEvent(ActorEvents.ACTOR_COMMAND_RECEIVE, args);
            }
        }

        private void UpdateHealthBar(OnActorHealthUpdated _args)
        {
            if (GetOwner == _args.actor)
            {
                int currentHealth = _args.healthComponent.GetCurrentHitPoints;
                int maxHealth = _args.healthComponent.GetMaxHitPoints;
                healthBarFillComponent.fillAmount = currentHealth / maxHealth;
            }
        }

        private void OnActorDeath(OnActorDeath _args)
        {
            if (GetOwner == _args.actor)
            {
                // TODO send to pull.
                Debug.LogError("Player is dead!!!");
            }
        }
    }
}
