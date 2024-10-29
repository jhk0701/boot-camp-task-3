using UnityEngine;

public class EquipableObject : ItemObject
{
    public override void Interact()
    {
        if (Player.Instance.inventory.IsFull())
        {
            // TODO UI 안내문 띄우기
            Debug.Log("인벤토리 슬롯이 모두 찼습니다.");
            return;
        }

        Player.Instance.inventory.AcquireItem(data);
        Destroy(gameObject);
    }
}