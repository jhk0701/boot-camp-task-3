using UnityEngine;
using TMPro;

[RequireComponent(typeof(Stat))]
public class UIStatusText : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    void Start()
    {
        GetComponent<Stat>().OnValueChanged += ChangeValue;
    }

    void ChangeValue(float val)
    {
        text.text = val.ToString();
    }
}