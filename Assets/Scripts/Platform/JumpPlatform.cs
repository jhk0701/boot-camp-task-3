using UnityEngine;

public class JumpPlatform : Platform
{
    [SerializeField] protected float power = 300f;

    protected override void Operate(Rigidbody rb)
    {
        rb.AddForce(Vector3.up * power, ForceMode.Impulse);
    }
}