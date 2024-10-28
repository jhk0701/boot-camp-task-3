using UnityEngine;
using UnityEngine.Events;

public enum InteractionType
{
    Launch,
}

public class InteractableObject : MonoBehaviour, IInteractable
{
    public KeyCode keyToInteract = KeyCode.E;
    public InteractionType type;

    public UnityEvent OnIteract;

    public void Interact()
    {
        OnIteract?.Invoke();
    }

    public string ConvertInteractionType(InteractionType type)
    {
        switch(type)
        {
            case InteractionType.Launch :
                return "발사하기";
            default :
                return "상호작용하기";
        }
    }
}
