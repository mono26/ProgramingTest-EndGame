using EndGame.Test.Actors;
using EndGame.Test.Events;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float bulletInitialSpeed = 3.0f;
    [SerializeField]
    private Rigidbody bulletBody;

    public void OnWeaponShoot()
    {
        bulletBody.AddForce(transform.forward * bulletInitialSpeed, ForceMode.Impulse);
    }

    private void OnTriggerEnter(Collider other)
    {
        Actor hitActor = other.GetComponent<Actor>();
        if (hitActor)
        {
            OnBulletHitActor args = new OnBulletHitActor()
            {
                actor = hitActor
            };

            EventController.QueueEvent(ActorEvents.ACTOR_HIT_BY_BULLET, args);
        }

        Destroy(gameObject);
    }
}
