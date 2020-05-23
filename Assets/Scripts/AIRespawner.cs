using EndGame.Test.Actors;
using EndGame.Test.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.AI
{
    public class AIRespawner : MonoBehaviour
    {
        private Action<IEventArgs> OnActorDeathEvent;

        [SerializeField]
        private Actor actorToRespawn = null;
        [SerializeField]
        private float respawnTime = 6.0f;

        private void Start()
        {
            OnActorDeathEvent = (args) => OnActorDeath((OnActorDeath)args);

            EventController.SubscribeToEvent(ActorEvents.ACTOR_DEATH, OnActorDeathEvent);
        }

        private void OnDestroy()
        {
            EventController.UnSubscribeFromEvent(ActorEvents.ACTOR_DEATH, OnActorDeathEvent);
        }

        private void OnActorDeath(OnActorDeath _args)
        {
            if (actorToRespawn == _args.actor)
            {
                StartCoroutine(StartRespawnCounter());
            }
        }

        private IEnumerator StartRespawnCounter()
        {
            yield return new WaitForSeconds(respawnTime);

            RespawnActor();
        }

        private void RespawnActor()
        {
            actorToRespawn.transform.position = transform.position;
            actorToRespawn.gameObject.SetActive(true);
        }
    }
}
