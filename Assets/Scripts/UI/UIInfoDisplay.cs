using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIInfoDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI infoTitleText;
    [SerializeField] TextMeshProUGUI infoDescText;


    void Start()
    {
        ClearInfo();
        
        Player.Instance.GetComponent<PlayerInteraction>().OnDetectItem += UpdateInfo;
    }

    public void UpdateInfo(ItemData data = null)
    {
        if(data == null)
        {
            ClearInfo();
            return;
        }

        infoTitleText.text = data.title;
        infoDescText.text = data.description;
    }

    public void ClearInfo()
    {
        infoTitleText.text = string.Empty;
        infoDescText.text = string.Empty;
    }

}
