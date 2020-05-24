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

        /// <summary>
        /// Called when the shooting animation reaches the last frame.
        /// </summary>
        public void OnFinishShootAnimtionEvent() => canShoot = true;

        /// <summary>
        /// Fire the weapon. Pools a bullet and the plays the sound and weapon vfx.
        /// </summary>
        public void FireWeapon()
        {
            if (canShoot)
            {
                Poolable bullet = PoolOfPools.GetObjectFromPool(bulletToFire);

                bullet.transform.position = bulletSpawnPoint.position;
                bullet.transform.rotation = bulletSpawnPoint.rotation;

                bullet.PoolExited();

                soundsComponent.PlayShootSound();

                effectsComponent.PlayParticleEffect();

                canShoot = false;
            }
        }
    }
}
