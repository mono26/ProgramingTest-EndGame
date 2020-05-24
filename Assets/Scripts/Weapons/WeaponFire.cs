using EndGame.Test.Game;
using UnityEngine;

namespace EndGame.Test.Weapons
{
    public class WeaponFire : MonoBehaviour
    {
        [SerializeField]
        private string bulletToFire = null;
        [SerializeField]
        private Transform bulletSpawnPoint = null;
        [SerializeField]
        private WeaponEffects effectsComponent = null;
        [SerializeField]
        private WeaponSounds soundsComponent = null;

        private bool canShoot = true;

        //private void Start()
        //{
        //    EventController.SubscribeToEvent(ActorEvents.ACTOR_TRIGGER_PULLED, (args) => OnActorPullingTrigger((OnActorPulledTrigger)args));
        //}

        //private void OnActorPullingTrigger(OnActorPulledTrigger _args)
        //{
        //    if (canShoot)
        //    {
        //        if (GetOwner == _args.actor)
        //        {
        //            FireWeapon(_args.aimDirection);
        //        }
        //    }
        //}

        public void OnFinishShootAnimtionEvent() => canShoot = true;

        public void FireWeapon(Vector3 _direction)
        {
            if (canShoot)
            {
                Poolable bullet = PoolOfPools.GetObjectFromPool(bulletToFire);

                bullet.transform.position = bulletSpawnPoint.position;
                Quaternion targetRotation = Quaternion.LookRotation(_direction, Vector3.up);
                bullet.transform.rotation = targetRotation;

                bullet.PoolExited();

                //Bullet newBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, targetRotation);
                //newBullet.OnWeaponShoot();

                soundsComponent.PlayShootSound();

                canShoot = false;
            }
        }
    }
}
