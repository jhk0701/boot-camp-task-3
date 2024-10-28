using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData data;

    [Space(10f)]
    [SerializeField] Image icon;

    public void SetUI()
    {
        if(data != null)
            icon.sprite = data.icon;
        else
            icon.sprite = null;
    }
}
