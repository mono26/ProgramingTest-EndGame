using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public event Action OnShootWeapon;

    [SerializeField]
    private WeaponFire weaponToShoot;

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            OnShootInput();
        }
    }

    private void OnShootInput()
    {
        weaponToShoot.FireWeapon();

        OnShootWeapon?.Invoke();
    }
}
