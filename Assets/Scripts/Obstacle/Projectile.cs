using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : MonoBehaviour
{
    Rigidbody rb;
    public float damage = 5f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    public void Fire(Vector3 force)
    {
        rb.AddForce(force, ForceMode.Impulse);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(damage);
        }
    }
}
