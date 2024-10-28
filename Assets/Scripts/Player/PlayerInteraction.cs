using System;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerInteraction : MonoBehaviour
{
    PlayerController controller;
    Camera cam;

    [Header("Interaction")]
    [SerializeField] float detectRate;
    [SerializeField] float maxDistance;
    [SerializeField] LayerMask interactLayerMask;

    float lastDetectTime;
    GameObject item;

    public event Action<ItemData> OnDetectItem;


    void Awake()
    {
        controller = GetComponent<PlayerController>();
        cam = Camera.main;
    }

    void Start()
    {
        // 생애 주기를 함께할 것이라 별도로 구독 해제는 구현하지 않음
        controller.OnInteractEvent += Interact;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {   
        if (Time.time - lastDetectTime <= detectRate)
            return;
        
        lastDetectTime = Time.time;
        Ray ray = cam.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0f));

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, interactLayerMask))
        {
            item = hit.collider.gameObject;
            if (item.TryGetComponent(out ItemObject itemObject))
            {
                UpdateUI(itemObject);
            }
        }
        else
        {
            OnDetectItem?.Invoke(null);
        }
    }

    
    void UpdateUI(ItemObject itemObject)
    {
        // Debug.Log($"{itemObject.data.title} / {itemObject.data.description}");
        OnDetectItem?.Invoke(itemObject.data);
    }

    void Interact()
    { 
        if (item == null) return;

        if (item.TryGetComponent(out IInteractable interactable))
        {
            interactable.Interact();
        }
    }
}