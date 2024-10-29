using System.Collections;
using UnityEngine;


public class Player : Singleton<Player>, IDamagable
{
    [HideInInspector] public PlayerController inputController;
    [HideInInspector] public PlayerInteraction interaction;
    [HideInInspector] public PlayerMovement movement;
    [HideInInspector] public Rigidbody rb;


    [Header("Status Stat")]
    // 인스펙터에서 직접 할당함
    public Stat health;
    public Stat stamina;
    public Stat mana;

    [Space(10f)]
    [Tooltip("프레임 당 스태미너 회복량")]
    [SerializeField] float staminaRecoverAmount;
    [SerializeField] float manaRecoverAmount;

    [Header("Ability Stat")]
    public Stat strength;
    public Stat defense;
    public Stat dexterity;

    [Header("Item")]
    // 해당 클래스에서 의존성 주입해줄 것.
    public UIConsumableItems consumableItems;
    
    WaitForSecondsRealtime waitForASecond = new WaitForSecondsRealtime(1f);


    void Awake()
    {
        inputController = GetComponent<PlayerController>();
        interaction = GetComponent<PlayerInteraction>();
        movement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }

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
        ItemEffect[] effects = item.itemEffects;
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