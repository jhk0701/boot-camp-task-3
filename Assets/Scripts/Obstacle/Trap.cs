using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    [SerializeField] Rigidbody trapRigidBody;
    [SerializeField] Vector3 direction;
    [SerializeField] float power;

    public void Activate()
    {
        if(trapRigidBody == null)
            return;

        trapRigidBody.AddForce(direction * power, ForceMode.Impulse);
    }

}
