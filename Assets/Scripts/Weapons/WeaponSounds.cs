using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.Weapons
{
    public class WeaponSounds : MonoBehaviour
    {
        [SerializeField]
        private AudioClip shootSound = null;
        [SerializeField]
        private AudioSource weaponSource = null;

        public void PlayShootSound()
        {
            AudioSource.PlayClipAtPoint(shootSound, transform.position);
        }
    }
}
