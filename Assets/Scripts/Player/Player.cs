using System.Collections;
using UnityEngine;


public class Player : MonoBehaviour, IDamagable
{
    [Header("Status Stat")]
    // 인스펙터에서 직접 할당함
    public StatusStat health;
    public StatusStat stamina;
    public StatusStat mana;

    [Space(10f)]
    [Tooltip("프레임 당 스태미너 회복량")]
    [SerializeField] float staminaRecoverAmount;
    [SerializeField] float manaRecoverAmount;

    [Header("Ability Stat")]
    public float strength;
    public float defense;
    public float dexterity;

    [Header("Item")]
    // 해당 클래스에서 의존성 주입해줄 것.
    public UIConsumableItems consumableItems;
    
    WaitForSecondsRealtime waitForASecond = new WaitForSecondsRealtime(1f);


    void Awake()
    {
        PlayerManager.Instance.player = this;
    }

    void Update()
    {
        RecoverStatusStat(stamina, staminaRecoverAmount * Time.deltaTime);
    }


    public void UseStatusStat(StatusStat stat, float amount)
    {
        stat.Subtract(amount);
    }

    public void RecoverStatusStat(StatusStat stat, float amount)
    {
        stat.Add(amount);
    }
    
    public void TakeDamage(float amount)
    {
        UseStatusStat(health, amount);
    }


    public void StartItemEffect(ItemData item)
    {
        if (ItemEffectHandler != null)
            StopCoroutine(ItemEffectHandler);
        
        ItemEffectHandler = StartCoroutine(AdjustItemEffect(item));
    }

    Coroutine ItemEffectHandler;
    IEnumerator AdjustItemEffect(ItemData item)
    {
        ConsumeEffect[] effects = item.consumeEffects;
        float elapsedTime = 0f;
        while (elapsedTime < item.duration)
        {
            for (int i = 0; i < effects.Length; i++)
            {
                switch(effects[i].target)
                {
                    case ItemEffectTarget.Health :
                        RecoverStatusStat(health, effects[i].effectValue);
                        break;
                    case ItemEffectTarget.Stamina :
                        RecoverStatusStat(stamina, effects[i].effectValue);
                        break;
                    case ItemEffectTarget.Mana :
                        RecoverStatusStat(mana, effects[i].effectValue);
                        break;
                }
            }
            yield return waitForASecond;
            elapsedTime += 1f;
        }

        ItemEffectHandler = null;
    }
}