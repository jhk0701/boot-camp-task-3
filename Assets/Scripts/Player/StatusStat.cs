using System;
using UnityEngine;

public class StatusStat : MonoBehaviour
{
    [SerializeField] float value;
    public float Value 
    { 
        get { return value; } 
        private set 
        {
            this.value = value;            
            OnValueChanged?.Invoke(this.value / max);
        }
    }
    [SerializeField] float max = 100f;
    [SerializeField] float min = 0f;

    public event Action<float> OnValueChanged;
    

    void Start()
    {
        Value = max;
    }


    public void Add(float amount)
    {
        if (amount <= 0f)
            return;

        Value = Mathf.Min(Value + amount, max);
    }
    
    public void Subtract(float amount)
    {
        if (amount <= 0f)
            return;

        Value = Mathf.Max(Value - amount, min);
    }
}