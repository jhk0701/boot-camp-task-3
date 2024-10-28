using UnityEngine;

public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData data;

    public void Interact()
    {
        if (PlayerManager.Instance.player.consumableItems.IsFull)
        {
            // TODO UI 안내문 띄우기
            Debug.Log("소비 아이템 슬롯이 모두 찼습니다.");
            return;
        }

        PlayerManager.Instance.player.consumableItems.AcquireItem(data);
        Destroy(gameObject);
    }

}