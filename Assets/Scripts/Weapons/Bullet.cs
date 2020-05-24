﻿using EndGame.Test.Actors;
using EndGame.Test.Events;
using EndGame.Test.Game;
using UnityEngine;

namespace EndGame.Test.Weapons
{
    public class Bullet : Poolable
    {
        [SerializeField]
        private float bulletInitialSpeed = 3.0f;
        [SerializeField]
        private Rigidbody bulletBody = null;

        public void OnWeaponShoot()
        {
            bulletBody.AddForce(transform.forward * bulletInitialSpeed, ForceMode.Impulse);
        }

        public override void PoolEntered()
        {
            bulletBody.velocity = Vector3.zero;
            bulletBody.angularVelocity = Vector3.zero;

            gameObject.SetActive(false);
        }

        public override void PoolExited()
        {
            gameObject.SetActive(true);

            OnWeaponShoot();
        }

        private void OnTriggerEnter(Collider other)
        {
            Actor hitActor = other.GetComponent<Actor>();
            if (hitActor)
            {
                OnBulletHitActor args = new OnBulletHitActor()
                {
                    actor = hitActor
                };

                EventController.QueueEvent(ActorEvents.ACTOR_HIT_BY_BULLET, args);
            }

            ReturnToPool();
        }
    }
}