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

        [SerializeField]
        private Image healthBarFillComponent = null;

        protected virtual void Start()
        {
            OnActorHealthUpdatedEvent = (args) => UpdateHealthBar((OnActorHealthUpdated)args);
            OnActorDeathEvent = (args) => OnActorDeath((OnActorDeath)args);

            EventController.SubscribeToEvent(ActorEvents.ACTOR_HEALTH_UPDATED, OnActorHealthUpdatedEvent);
            EventController.SubscribeToEvent(ActorEvents.ACTOR_DEATH, OnActorDeathEvent);
        }

        private void OnDestroy()
        {
            EventController.UnSubscribeFromEvent(ActorEvents.ACTOR_HEALTH_UPDATED, OnActorHealthUpdatedEvent);
            EventController.UnSubscribeFromEvent(ActorEvents.ACTOR_DEATH, OnActorDeathEvent);
        }

        private void UpdateHealthBar(OnActorHealthUpdated _args)
        {
            if (GetOwner == _args.actor)
            {
                int currentHealth = _args.healthComponent.GetCurrentHitPoints;
                int maxHealth = _args.healthComponent.GetMaxHitPoints;
                healthBarFillComponent.fillAmount = (float)currentHealth / (float)maxHealth;
            }
        }

        protected abstract void OnActorDeath(OnActorDeath _args);
    }
}
