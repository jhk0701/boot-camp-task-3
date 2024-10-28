using UnityEngine;
using TMPro;
using System;

public class UIInfoDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI infoTitleText;
    [SerializeField] TextMeshProUGUI infoDescText;


    void Start()
    {
        ClearInfo();

        PlayerInteraction interaction = Player.Instance.interaction;
        interaction.OnDetectItem += UpdateInfo;
        interaction.OnDetectObject += UpdateInfo;
    }

    void UpdateInfo(ItemData data = null)
    {
        if(data == null)
        {
            ClearInfo();
            return;
        }

        infoTitleText.text = data.title;
        infoDescText.text = data.description;
    }

    void UpdateInfo(InteractableObject data = null)
    {
        if(data == null)
        {
            ClearInfo();
            return;
        }
        infoTitleText.text = string.Empty;
        infoDescText.text = String.Format("{0}키를 눌러 {1}.", data.keyToInteract.ToString(), data.ConvertInteractionType(data.type));
    }

    void ClearInfo()
    {
        infoTitleText.text = string.Empty;
        infoDescText.text = string.Empty;
    }

}
