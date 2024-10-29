using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Detector : MonoBehaviour
{
    [SerializeField] LayerMask detectingMask;
    [SerializeField] float detectRate = 0.2f;
    float lastDetectTime = 0f;
    [SerializeField] float detectDistance = 5f;
    [SerializeField] Transform detectPoint;

    public UnityEvent OnDetect;

    void Start()
    {
        if(detectPoint == null)
            detectPoint = transform;

        // OnDetect.AddListener(()=>{Debug.Log("Player is detected"); });
    }

    void Update()
    {
        if(Time.time - lastDetectTime > detectRate)
        {
            lastDetectTime = Time.time;
            Ray ray = new Ray(detectPoint.position, detectPoint.forward);
            if (Physics.Raycast(ray, detectDistance, detectingMask))
            {
                OnDetect?.Invoke();
            }
        }
    }
}
