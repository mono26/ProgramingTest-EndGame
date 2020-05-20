using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    [SerializeField]
    private WeaponFire weaponToShoot;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            OnShootInput();
        }
    }

    private void OnShootInput()
    {
        weaponToShoot.FireWeapon();
    }
}
