using EndGame.Test.Actors;
using EndGame.Test.Events;
using UnityEngine;

public class WeaponFire : ActorComponent
{
    [SerializeField]
    private float fireRate = 1.0f;
    [SerializeField]
    private Bullet bulletPrefab = null;
    [SerializeField]
    private Transform bulletSpawnPoint = null;

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
        Quaternion targetRotation = Quaternion.LookRotation(_direction, Vector3.up);
        Bullet newBullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, targetRotation);
        newBullet.OnWeaponShoot();

        canShoot = false;
    }
}
