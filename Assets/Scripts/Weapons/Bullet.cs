using System.Collections;
using System.Collections.Generic;
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
        Destroy(gameObject);
    }
}
