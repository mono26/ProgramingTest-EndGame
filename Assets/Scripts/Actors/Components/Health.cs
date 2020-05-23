using EndGame.Test.Events;
using UnityEngine;

namespace EndGame.Test.Actors
{
    public class Health : ActorComponent
    {
        [SerializeField]
        private int hitPoints = 3;

        private void Start()
        {
            EventController.SubscribeToEvent(ActorEvents.ACTOR_HIT_BY_BULLET, (args) => OnBulletHitActor((OnBulletHitActor)args));
        }

        private void OnBulletHitActor(OnBulletHitActor _args)
        {
            if (GetOwner == _args.actor)
            {
                hitPoints--;
            }
        }
    }
}
