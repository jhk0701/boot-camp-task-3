using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public void Interact()
    {
        Debug.Log($"Interact {name}");
        Destroy(gameObject);
    }

}