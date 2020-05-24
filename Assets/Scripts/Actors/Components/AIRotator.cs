using EndGame.Test.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.Actors
{
    public class AIRotator : Rotator
    {
        protected override void Start()
        {
            //EventController.SubscribeToEvent(ActorEvents.ACTOR_FIRE_WEAPON, (args) => OnActorFireWeapon((OnActorFireWeapon)args));
        }

        protected override void OnDestroy()
        {
            // TODO store as delegates ??
            //EventController.UnSubscribeFromEvent(ActorEvents.ACTOR_FIRE_WEAPON, (args) => OnActorFireWeapon((OnActorFireWeapon)args));
        }
    }
}
