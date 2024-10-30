using System;
using UnityEngine;

[RequireComponent(typeof(PlayerInputController))]
public class PlayerInteraction : MonoBehaviour
{
    Camera cam;

    [Header("Interaction")]
    [SerializeField] float detectRate;
    [SerializeField] float maxDistance;
    [SerializeField] LayerMask interactLayerMask;

    float lastDetectTime;
    GameObject detectedObject;

    public event Action<ItemData> OnDetectItem;
    public event Action<InteractableObject> OnDetectObject;


    void Start()
    {
        cam = Camera.main;

        // 생애 주기를 함께할 것이라 별도로 구독 해제는 구현하지 않음
        PlayerInputController input = Player.Instance.inputController;
        input.OnInteractEvent += Interact;
    }

    void Update()
    {   
        if (Time.time - lastDetectTime <= detectRate)
            return;
        
        lastDetectTime = Time.time;
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, interactLayerMask))
        {
            detectedObject = hit.collider.gameObject;
            if (detectedObject.TryGetComponent(out ItemObject itemObject))
            {
                OnDetectItem?.Invoke(itemObject.data);
                
            }
            else if (detectedObject.TryGetComponent(out InteractableObject interactableObject))
            {
                OnDetectObject?.Invoke(interactableObject);
            }
        }
        else
        {
            OnDetectItem?.Invoke(null);
            OnDetectObject?.Invoke(null);
        }
    }

    void Interact()
    { 
        if(detectedObject == null) 
            return;

        if (detectedObject.TryGetComponent(out IInteractable interactable))
        {
            interactable.Interact();
        }
        
    }
}