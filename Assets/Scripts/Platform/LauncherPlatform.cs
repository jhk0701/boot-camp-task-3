using UnityEngine;

public class LauncherPlatform : JumpPlatform
{
    [SerializeField] Transform firePoint;
    Rigidbody rb;

    protected override void Operate(Rigidbody rb)
    {
        this.rb = rb;
        Invoke("Launch", 5f);
    }

    public void Launch()
    {
        if(rb == null)
            return;

        if (IsInvoking("Launch"))
            CancelInvoke("Launch");

        rb.AddForce((firePoint ? firePoint.forward : transform.forward) * power, ForceMode.Impulse);
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