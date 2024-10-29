using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(Stat))]
public class UIStatusBar : MonoBehaviour
{
    [SerializeField] Image fillBar;

    void Start()
    {
        if(fillBar == null)
            fillBar = GetComponentInChildren<Image>();

        GetComponent<Stat>().OnValueChanged += ChangeValue;
    }

    void ChangeValue(float val, float max)
    {
        fillBar.fillAmount = val / max;
    }
}
