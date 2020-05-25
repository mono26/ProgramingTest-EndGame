using EndGame.Test.Events;
using System;
using UnityEngine;

namespace EndGame.Test.Actors
{
    public class Health : ActorComponent
    {
        private Action<IEventArgs> OnBulletHitActorListener;
        private Action<IEventArgs> OnActorRespawnListener;

        /// <summary>
        /// Max health value.
        /// </summary>
        [SerializeField]
        private int maxHitPoints = 3;
        /// <summary>
        /// The current value of the hit points.
        /// </summary>
        [SerializeField]
        private int currentHitPoints = 3;

        public int GetMaxHitPoints { get => maxHitPoints; }
        public int GetCurrentHitPoints { get => currentHitPoints; }

        private void Start()
        {
            currentHitPoints = maxHitPoints;

            OnBulletHitActorListener = (args) => OnBulletHitActor((OnActorEventEventArgs)args);
            OnActorRespawnListener = (args) => OnActorRespawn((OnActorEventEventArgs)args);

            EventController.SubscribeToEvent(ActorEvents.ACTOR_HIT_BY_BULLET, OnBulletHitActorListener);
            EventController.SubscribeToEvent(ActorEvents.ACTOR_RESPAWN, OnActorRespawnListener);
        }

        private void OnDestroy()
        {
            EventController.UnSubscribeFromEvent(ActorEvents.ACTOR_HIT_BY_BULLET, OnBulletHitActorListener);
            EventController.UnSubscribeFromEvent(ActorEvents.ACTOR_RESPAWN, OnActorRespawnListener);
        }

        /// <summary>
        /// Updates the healh value after a bullet hit. Also sends health update events and death event if the health reaches zero.
        /// </summary>
        /// <param name="_args">Bullet hit args.</param>
        private void OnBulletHitActor(OnActorEventEventArgs _args)
        {
            if (GetOwner == _args.actor)
            {
                currentHitPoints--;

                if (currentHitPoints >= 0)
                {
                    OnActorHealthUpdated args = new OnActorHealthUpdated()
                    {
                        baseArgs = new OnActorEventEventArgs() { actor = GetOwner },
                        healthComponent = this
                    };

                    EventController.QueueEvent(ActorEvents.ACTOR_HEALTH_UPDATED, args);
                }
                else
                {
                    OnActorEventEventArgs args = new OnActorEventEventArgs()
                    {
                        actor = GetOwner
                    };

                    EventController.PushEvent(ActorEvents.ACTOR_DEATH, args);
                }
            }
        }

        /// <summary>
        /// Updates the health value to the max one after the actor respawns.
        /// </summary>
        /// <param name="_args"></param>
        private void OnActorRespawn(OnActorEventEventArgs _args)
        {
            if (GetOwner == _args.actor)
            {
                currentHitPoints = maxHitPoints;

                OnActorHealthUpdated args = new OnActorHealthUpdated()
                {
                    baseArgs = new OnActorEventEventArgs() { actor = GetOwner },
                    healthComponent = this
                };

                EventController.QueueEvent(ActorEvents.ACTOR_HEALTH_UPDATED, args);
            }
        }
    }
}
