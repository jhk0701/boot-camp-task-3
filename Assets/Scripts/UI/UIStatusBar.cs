using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(StatusStat))]
public class UIStatusBar : MonoBehaviour
{
    Image fillBar;

    void Awake()
    {
        fillBar = GetComponent<Image>();
    }

    void Start()
    {
        GetComponent<StatusStat>().OnValueChanged += ChangeValue;
    }

    void ChangeValue(float val)
    {
        fillBar.fillAmount = val;
    }
}
