using EndGame.Test.Actors;
using EndGame.Test.Events;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponFire : ActorComponent
{
    [SerializeField]
    private float fireRate = 1.0f;
    [SerializeField]
    private Bullet bulletPrefab = null;
    [SerializeField]
    private Transform bulletSpawnPoint = null;

    private void Start()
    {
        EventController.SubscribeToEvent(ActorEvents.ACTOR_TRIGGER_PULLED, (args) => OnActorPullingTrigger((OnActorPulledTrigger)args));
    }

    private void OnActorPullingTrigger(OnActorPulledTrigger _args)
    {
        if (GetOwner == _args.actor)
        {
            FireWeapon(_args.aimDirection);
        }
    }

    public void FireWeapon(Vector3 _direction)
    {
        Quaternion targetRotation = Quaternion.FromToRotation(_direction, Vector3.up);
        Bullet newBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, targetRotation);
        newBullet.OnWeaponShoot();
    }
}
