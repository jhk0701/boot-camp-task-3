using UnityEngine;

public class HoldingPlatform : Platform
{
    Transform player;

    protected override void Operate(Rigidbody rb)
    {
        // 탑승
        player = rb.transform;
        player.transform.SetParent(transform);
    }

    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            // 하차
            player.transform.SetParent(null);
            player = null;
        }
    }
}
