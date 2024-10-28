using UnityEngine;

public abstract class Platform : MonoBehaviour
{    
    protected abstract void Operate(Player p);

    protected virtual void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            Operate(player);
        }
    }
}
