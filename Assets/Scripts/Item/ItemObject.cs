using UnityEngine;

public abstract class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;
    public abstract void Interact();
}