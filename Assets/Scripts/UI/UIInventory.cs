using UnityEngine;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;
    public ItemSlot[] currentEquipped;

    [SerializeField] GameObject panel;

    void Awake()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i].index = i;
            slots[i].OnSlotSelected += Equip;
        }

        for (int i = 0; i < currentEquipped.Length; i++)
        {
            currentEquipped[i].index = i;
            currentEquipped[i].OnSlotSelected += Unequip;
        }
    }

    void Start()
    {
        Player.Instance.inventory = this;

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
        {
            ThrowItem(item);
            return;
        }

        slot.data = item;

        UpdateInventoryUI();
    }

    void UpdateInventoryUI()
    {
        for (int i = 0; i < slots.Length; i++)
            slots[i].SetUI();
    }

    void UpdateEquipmentUI()
    {
        for (int i = 0; i < currentEquipped.Length; i++)
            currentEquipped[i].SetUI();
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
        Vector3 pos = transform.position + Vector3.forward;
        Instantiate(item.prefab, pos, Quaternion.identity);
    }

    public void Equip(int index)
    {
        ItemSlot slot = GetEmptyEquipSlot();

        if(slot == null)
        {
            Debug.Log("장착 아이템 창이 모두 찼습니다.");
            return;
        }
        
        slot.data = slots[index].data;
        slots[index].data = null;

        Player.Instance.status.AdjustEquipment(slot.data);

        UpdateInventoryUI();
        UpdateEquipmentUI();
    }

    public void Unequip(int index)
    {
        ItemSlot slot = GetEmptySlot();

        if(slot == null)
        {
            Debug.Log("아이템 창이 모두 찼습니다.");
            return;
        }
        
        slot.data = currentEquipped[index].data;
        currentEquipped[index].data = null;

        Player.Instance.status.RemoveEquipment(slot.data);

        UpdateInventoryUI();
        UpdateEquipmentUI();
    }

    ItemSlot GetEmptyEquipSlot()
    {
        for (int i = 0; i < currentEquipped.Length; i++)
        {
            if(currentEquipped[i].data == null)
                return currentEquipped[i];
        }

        return null;
    }
}