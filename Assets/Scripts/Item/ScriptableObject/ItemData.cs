using System;
using UnityEngine;

public enum ItemType
{
    Consumable,// 소비 아이템
    Equipable, // 장비 아이템
}

public enum ItemEffectTarget
{
    Health,
    Stamina,
    Mana,

    Strength,
    Defense,
    Dexterity
}

public enum ItemEffectType
{
    Recovery,
    InstantSupport
}

[Serializable]
public class ItemEffect
{
    public ItemEffectTarget target;
    public ItemEffectType type;
    public float effectValue;
}

[CreateAssetMenu(fileName = "Item",menuName = "boot-camp-task-3/ItemData")]
public class ItemData : ScriptableObject
{
    public string title;
    public string description;
    public ItemType type;
    public Sprite icon;
    public float duration;

    [Header("Item Effect")]
    public ItemEffect[] itemEffects;
}
