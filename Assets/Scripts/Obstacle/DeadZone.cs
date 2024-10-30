using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent(out IDamagable damagable))
        {
            damagable.TakeDamage(float.MaxValue);
        }
    }
}
