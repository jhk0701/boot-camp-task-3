using System;
using UnityEngine;

public class Stat : MonoBehaviour
{
    [SerializeField] float value;
    public float Value 
    { 
        get { return value; } 
        private set 
        {
            this.value = value;
            if(this.value < 0f)
                this.value = 0f;

            OnValueChanged?.Invoke(this.value, max);
        }
    }
    [SerializeField] float startValue = 100f;
    [SerializeField] float max = 100f;
    [SerializeField] float min = 0f;

    public event Action<float, float> OnValueChanged;
    

    void Start()
    {
        Initialize();
    }


    public void Initialize()
    {
        Value = startValue;
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