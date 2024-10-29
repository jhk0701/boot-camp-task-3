using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public enum InteractionType
{
    Launch,
    // OpenDoor : 문 열기 등등
}

public class InteractableObject : MonoBehaviour, IInteractable
{
    // e키로 강제 고정
    [HideInInspector] public KeyCode keyToInteract = KeyCode.E; 
    public InteractionType type;

    [Space(10f)]
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
