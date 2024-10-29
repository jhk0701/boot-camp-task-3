using UnityEngine;

public class ConsumableObject : ItemObject
{
    public override void Interact()
    {
        if (Player.Instance.consumableItems.IsFull)
        {
            // TODO UI 안내문 띄우기
            Debug.Log("소비 아이템 슬롯이 모두 찼습니다.");
            return;
        }

        Player.Instance.consumableItems.AcquireItem(data);
        Destroy(gameObject);
    }
}