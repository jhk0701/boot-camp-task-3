using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField] ItemData[] items;
    [SerializeField] int count = 1;

    public void Open()
    {   
        Vector3 basePos = transform.position;

        for (int i = 0; i < count; i++)
        {
            Instantiate(items[Random.Range(0, items.Length)].prefab, basePos + Vector3.right * Random.Range(-1f,1f), Quaternion.identity);    
        }

        Destroy(gameObject);
    }
}
