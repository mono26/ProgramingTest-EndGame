using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : MonoBehaviour
{
    [SerializeField]
    private float fireRate = 1.0f;
    [SerializeField]
    private Bullet bulletPrefab = null;
    [SerializeField]
    private Transform bulletSpawnPoint = null;

    public void FireWeapon()
    {
        Bullet newBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        newBullet.OnWeaponShoot();
    }
}
