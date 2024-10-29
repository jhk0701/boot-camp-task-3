using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;
    public ItemSlot[] currentEquipped;

    [SerializeField] GameObject panel;

    void Awake()
    {
        for (int i = 0; i < slots.Length; i++)
            slots[i].index = i;

        for (int i = 0; i < currentEquipped.Length; i++)
            currentEquipped[i].index = i;
    }

    void Start()
    {
        Player.Instance.inputController.OnInventoryEvent += Toggle;
    }


    public void Toggle()
    {
        panel.SetActive(!panel.activeInHierarchy);
    }

    public void AcquireItem(ItemData item)
    {
        // 빈칸 가져오기
        ItemSlot slot = GetEmptySlot();

        if (slot == null)
            return;

        slot.data = item;

        UpdateInventoryUI();
    }

    void UpdateInventoryUI()
    {
        for (int i = 0; i < slots.Length; i++)
            slots[i].SetUI();
    }

    public bool IsFull()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if(slots[i].data == null)
                return false;
        }

        return true;
    }

    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].data == null)
                return slots[i];
        }

        return null;
    }

    public void ThrowItem(ItemData item)
    {

    }
}