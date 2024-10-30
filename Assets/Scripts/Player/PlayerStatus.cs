using System.Collections;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class PlayerStatus : MonoBehaviour, IDamagable
{
    
    [Header("Status Stat")]
    // 인스펙터에서 직접 할당함
    public Stat health;
    public Stat stamina;
    public Stat mana;

    [Header("Ability Stat")]
    public Stat strength;
    public Stat defense;
    public Stat dexterity;

    [Space(10f)]
    [Tooltip("프레임 당 스태미너 회복량")]
    [SerializeField] float staminaRecoverAmount;
    [SerializeField] float manaRecoverAmount;

    WaitForSecondsRealtime waitForASecond = new WaitForSecondsRealtime(1f);


    void Update()
    {
        RecoverStatusStat(stamina, staminaRecoverAmount * Time.deltaTime);
    }


    public void UseStatusStat(Stat stat, float amount)
    {
        stat.Subtract(amount);
    }

    public void RecoverStatusStat(Stat stat, float amount)
    {
        stat.Add(amount);
    }
    
    public void TakeDamage(float amount)
    {
        float defenseValue = defense.Value / 3;
        float damage = defenseValue >= amount ? 0f : amount - defenseValue;
        UseStatusStat(health, damage);
    }


    public void StartItemEffect(ItemData item)
    {
        if (ItemEffectHandler != null)
            StopCoroutine(ItemEffectHandler);
        
        ItemEffectHandler = StartCoroutine(AdjustItemEffect(item));
    }

    // TODO : 리팩토링 반드시 할 것
    Coroutine ItemEffectHandler;
    IEnumerator AdjustItemEffect(ItemData item)
    {
        float elapsedTime = 0f;
        List<ItemEffect> recover = new List<ItemEffect>();
        List<ItemEffect> instant = new List<ItemEffect>();

        for(int i = 0; i < item.itemEffects.Length; i++)
        {
            switch(item.itemEffects[i].type)
            {
                case ItemEffectType.Recovery :
                    recover.Add(item.itemEffects[i]);
                    break;
                case ItemEffectType.InstantSupport :
                    instant.Add(item.itemEffects[i]);
                    break;
            }
        }

        foreach (ItemEffect effect in instant)
        {
            switch(effect.target)
            {
                case ItemEffectTarget.Strength :
                    strength.Add(effect.effectValue);
                    break;
                case ItemEffectTarget.Defense :
                    defense.Add(effect.effectValue);
                    break;
                case ItemEffectTarget.Dexterity :
                    dexterity.Add(effect.effectValue);
                    break;
            }
        }

        while (elapsedTime < item.duration)
        {
            for (int i = 0; i < recover.Count; i++)
            {
                switch(recover[i].target)
                {
                    case ItemEffectTarget.Health :
                        RecoverStatusStat(health, recover[i].effectValue);
                        break;
                    case ItemEffectTarget.Stamina :
                        RecoverStatusStat(stamina, recover[i].effectValue);
                        break;
                    case ItemEffectTarget.Mana :
                        RecoverStatusStat(mana, recover[i].effectValue);
                        break;
                }
            }

            yield return waitForASecond;

            elapsedTime += 1f;
        }

        foreach (ItemEffect effect in instant)
        {
            switch(effect.target)
            {
                case ItemEffectTarget.Strength :
                    strength.Subtract(effect.effectValue);
                    break;
                case ItemEffectTarget.Defense :
                    defense.Subtract(effect.effectValue);
                    break;
                case ItemEffectTarget.Dexterity :
                    dexterity.Subtract(effect.effectValue);
                    break;
            }
        }

        ItemEffectHandler = null;
    }

    public void AdjustEquipment(ItemData data)
    {
        for (int i = 0; i < data.itemEffects.Length; i++)
        {
            switch (data.itemEffects[i].target)
            {
                case ItemEffectTarget.Strength :
                    strength.Add(data.itemEffects[i].effectValue);
                    break;
                case ItemEffectTarget.Defense :
                    defense.Add(data.itemEffects[i].effectValue);
                    break;
                case ItemEffectTarget.Dexterity :
                    dexterity.Add(data.itemEffects[i].effectValue);
                    break;
            }
        }
    }

    public void RemoveEquipment(ItemData data)
    {
        for (int i = 0; i < data.itemEffects.Length; i++)
        {
            switch (data.itemEffects[i].target)
            {
                case ItemEffectTarget.Strength :
                    strength.Subtract(data.itemEffects[i].effectValue);
                    break;
                case ItemEffectTarget.Defense :
                    defense.Subtract(data.itemEffects[i].effectValue);
                    break;
                case ItemEffectTarget.Dexterity :
                    dexterity.Subtract(data.itemEffects[i].effectValue);
                    break;
            }
        }
    }
}