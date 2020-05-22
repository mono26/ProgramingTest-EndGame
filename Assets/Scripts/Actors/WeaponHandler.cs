using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public event Action OnPullTrigger;
    public event Action OnReleaseTrigger;

    [SerializeField]
    private WeaponFire weaponToShoot;

    private void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            OnShootInput();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            OnReleaseShootInput();
        }
    }

    private void OnShootInput()
    {
        //weaponToShoot.FireWeapon();

        OnPullTrigger?.Invoke();
    }

    private void OnReleaseShootInput()
    {
        OnReleaseTrigger?.Invoke();
    }
}
