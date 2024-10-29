using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData data;
    public int index;

    [Space(10f)]
    [SerializeField] Sprite defaultIcon;
    [SerializeField] Image icon;

    public event Action<int> OnSlotSelected;

    public void SetUI()
    {
        if(data != null)
            icon.sprite = data.icon;
        else
            icon.sprite = defaultIcon;
    }

    public void Select()
    {
        OnSlotSelected?.Invoke(index);
    }
}
