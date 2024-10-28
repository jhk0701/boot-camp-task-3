using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(StatusStat))]
public class UIStatusBar : MonoBehaviour
{
    [SerializeField] Image fillBar;

    void Awake()
    {
        if(fillBar == null)
            fillBar = GetComponentInChildren<Image>();
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
