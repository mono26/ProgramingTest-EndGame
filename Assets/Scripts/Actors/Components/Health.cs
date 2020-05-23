using EndGame.Test.Events;
using UnityEngine;

namespace EndGame.Test.Actors
{
    public class Health : ActorComponent
    {
        [SerializeField]
        private int maxHitPoints = 3;

        private int currentHitPoints = 3;

        public int GetMaxHitPoints { get => maxHitPoints; }
        public int GetCurrentHitPoints { get => currentHitPoints; }

        private void Start()
        {
            currentHitPoints = maxHitPoints;

            EventController.SubscribeToEvent(ActorEvents.ACTOR_HIT_BY_BULLET, (args) => OnBulletHitActor((OnBulletHitActor)args));
        }

        private void OnBulletHitActor(OnBulletHitActor _args)
        {
            if (GetOwner == _args.actor)
            {
                currentHitPoints--;

                if (currentHitPoints > 0)
                {
                    OnActorHealthUpdated args = new OnActorHealthUpdated()
                    {
                        actor = GetOwner,
                        healthComponent = this
                    };

                    EventController.PushEvent(ActorEvents.ACTOR_HEALTH_UPDATED, args);
                }
                else
                {
                    OnActorDeath args = new OnActorDeath()
                    {
                        actor = GetOwner
                    };

                    EventController.PushEvent(ActorEvents.ACTOR_DEATH, args);
                }
            }
        }
    }
}
