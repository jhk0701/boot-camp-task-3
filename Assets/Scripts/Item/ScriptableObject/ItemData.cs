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

    Strength,
    Defense,
    Dexterity
}

[Serializable]
public class ConsumeEffect
{
    public ItemEffectTarget target;
    public float effectValue;
    public float duration;
}

[CreateAssetMenu(fileName = "Item",menuName = "boot-camp-task-3/ItemData")]
public class ItemData : ScriptableObject
{
    public string title;
    public string description;
    public ItemType type;
    public Sprite icon;

    [Header("Consume Effect")]
    public ConsumeEffect[] consumeEffects;
}
