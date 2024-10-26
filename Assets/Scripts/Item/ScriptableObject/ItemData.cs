using UnityEngine;

public enum ItemType
{
    Consumable,// 소비 아이템
    Equipable, // 장비 아이템
}

[CreateAssetMenu(fileName = "Item",menuName = "boot-camp-task-3/ItemData")]
public class ItemData : ScriptableObject
{
    public string title;
    public string description;
    public ItemType type;

}
