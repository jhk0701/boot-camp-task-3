using System.ComponentModel;
using UnityEngine;
using UnityEngine.Events;

public enum InteractionType
{
    Launch,
    Open, // OpenDoor : 문 열기 등등
    
}

public class InteractableObject : MonoBehaviour, IInteractable
{
    // e키로 강제 고정
    [HideInInspector] public KeyCode keyToInteract = KeyCode.E; 
    public InteractionType type;

    [Space(10f)]
    public UnityEvent OnInteract;

    public void Interact()
    {
        OnInteract?.Invoke();
    }

    public string ConvertInteractionType(InteractionType type)
    {
        switch(type)
        {
            case InteractionType.Launch :
                return "발사하기";
            case InteractionType.Open :
                return "열기";
            default :
                return "상호작용하기";
        }
    }
}
