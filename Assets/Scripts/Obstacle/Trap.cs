using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] Projectile projectile;
    [SerializeField] Transform firePoint;
    [SerializeField] float power;

    public void Activate()
    {
        if (projectile == null)
            return;

        if (firePoint == null)
            firePoint = transform;

        projectile.Fire(firePoint.forward * power);
    }
}
