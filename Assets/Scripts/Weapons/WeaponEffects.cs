using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndGame.Test.Weapons
{
    public class WeaponEffects : MonoBehaviour
    {
        [SerializeField]
        private ParticleSystem shootParticles;

        public void PlayParticleEffect()
        {
            shootParticles.Play();
        }
    }
}
