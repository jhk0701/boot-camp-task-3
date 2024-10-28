using UnityEngine;


public class Player : MonoBehaviour, IDamagable
{
    [Header("Status Stat")]
    // 인스펙터에서 직접 할당함
    public StatusStat health;
    public StatusStat stamina;

    [Tooltip("프레임 당 스태미너 회복량")]
    [SerializeField] float staminaRecoverAmount;

    [Header("Ability Stat")]
    public float strength;
    public float defense;
    public float dexterity;

    // 해당 클래스에서 의존성 주입해줄 것.
    public UIConsumableItems consumableItems;
    


    void Awake()
    {
        PlayerManager.Instance.player = this;
    }

    void Update()
    {
        RecoverStamina(staminaRecoverAmount * Time.deltaTime);
    }


    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void TakeDamage(float amount)
    {
        health.Subtract(amount);
    }

    public void RecoverStamina(float amount)
    {
        stamina.Add(amount);
    }

    public void UseStamina(float amount)
    {
        stamina.Subtract(amount);
    }
}