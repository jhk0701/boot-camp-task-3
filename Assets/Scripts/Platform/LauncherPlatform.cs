using UnityEngine;

public class LauncherPlatform : JumpPlatform, IInteractable
{
    [SerializeField] Transform firePoint;

    public void Interact()
    {
        
    }

    protected override void Operate(Player player)
    {
        player.rb.AddForce((firePoint ? firePoint.forward : transform.forward)  * power, ForceMode.Impulse);
    }
}