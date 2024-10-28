using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIConsumableItems : MonoBehaviour
{
    Queue<ItemData> datas = new Queue<ItemData>();    

    public ItemSlot[] slots;
    public bool IsFull => datas.Count == slots.Length;


    void Start()
    {
        PlayerManager.Instance.player.consumableItems = this;
        // PlayerManager.Instance.player.
    }

    public void AcquireItem(ItemData item)
    {
        datas.Enqueue(item);

        UpdateUI();
    }

    public void UseItem()
    {

    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i <= datas.Count - 1)
                slots[i].data = datas.ElementAt(i);
            else
                slots[i].data = null;

            slots[i].SetUI();
        }
    }
}