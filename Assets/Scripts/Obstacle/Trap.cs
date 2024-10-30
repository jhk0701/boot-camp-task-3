using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] Projectile projectile;
    [SerializeField] Transform firePoint;
    [SerializeField] float power;
    [SerializeField] bool isFired = false;

    void Start()
    {
        isFired = false;
    }

    public void Activate()
    {
        if (projectile == null || isFired)
            return;

        if (firePoint == null)
            firePoint = transform;

        projectile.Fire(firePoint.forward * power);
        isFired = true;
    }
}
