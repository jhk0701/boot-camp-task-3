using UnityEngine;

public class LauncherPlatform : JumpPlatform
{
    [SerializeField] Transform fireDirection;
    [SerializeField] float delayTime = 5f;
    Rigidbody rb;

    protected override void Operate(Rigidbody rb)
    {
        this.rb = rb;
        Invoke("Launch", delayTime);
    }

    public void Launch()
    {
        if(rb == null)
            return;

        if (IsInvoking("Launch"))
            CancelInvoke("Launch");

        rb.AddForce((fireDirection ? fireDirection.forward : transform.forward) * power, ForceMode.Impulse);
        rb = null;
    }

    void OnCollisionExit(Collision other)
    {
        if (IsInvoking("Launch"))
            CancelInvoke("Launch");

        if (rb != null)
            rb = null;
    }
}