using EndGame.Test.Events;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace EndGame.Test.Actors
{
    public abstract class ActorView : ActorComponent
    {
        private Action<IEventArgs> OnActorHealthUpdatedEvent;
        private Action<IEventArgs> OnActorDeathEvent;

        /// <summary>
        /// Fill image component.
        /// </summary>
        [SerializeField]
        private Image healthBarFillComponent = null;

        protected virtual void Start()
        {
            OnActorHealthUpdatedEvent = (args) => UpdateHealthBar((OnActorHealthUpdated)args);
            OnActorDeathEvent = (args) => OnActorDeath((OnActorEventEventArgs)args);

            EventController.SubscribeToEvent(ActorEvents.ACTOR_HEALTH_UPDATED, OnActorHealthUpdatedEvent);
            EventController.SubscribeToEvent(ActorEvents.ACTOR_DEATH, OnActorDeathEvent);
        }

        private void OnDestroy()
        {
            EventController.UnSubscribeFromEvent(ActorEvents.ACTOR_HEALTH_UPDATED, OnActorHealthUpdatedEvent);
            EventController.UnSubscribeFromEvent(ActorEvents.ACTOR_DEATH, OnActorDeathEvent);
        }

        /// <summary>
        /// Updates the healthbar of the actor to reflect the current value.
        /// </summary>
        /// <param name="_args"></param>
        private void UpdateHealthBar(OnActorHealthUpdated _args)
        {
            if (GetOwner == _args.baseArgs.actor)
            {
                int currentHealth = _args.healthComponent.GetCurrentHitPoints;
                int maxHealth = _args.healthComponent.GetMaxHitPoints;
                healthBarFillComponent.fillAmount = (float)currentHealth / (float)maxHealth;
            }
        }

        /// <summary>
        /// Called when the actor dies.
        /// </summary>
        /// <param name="_args"></param>
        protected abstract void OnActorDeath(OnActorEventEventArgs _args);
    }
}
