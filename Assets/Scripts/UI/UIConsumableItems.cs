using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UIConsumableItems : MonoBehaviour
{
    Queue<ItemData> datas = new Queue<ItemData>();    

    public ItemSlot[] slots;
    public bool IsFull => datas.Count == slots.Length;

    bool isInCooldown = false;
    [SerializeField] Image cooldownBar;


    void Start()
    {
        Player.Instance.consumableItems = this;
        
        Player.Instance.inputController.OnUseItemEvent += UseItem;
    }

    public void AcquireItem(ItemData item)
    {
        datas.Enqueue(item);

        UpdateUI();
    }

    public void UseItem()
    {
        if (datas.Count == 0 || isInCooldown)
             return;

        // 맨 처음 item 선택
        ItemData data = datas.Dequeue();

        // 사용
        Player.Instance.status.StartItemEffect(data);
        
        // 아이템 쿨타임 적용
        StartCooldown(data.duration);

        // 슬롯 표기 정리
        UpdateUI();
    }

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (i <= datas.Count - 1)
                slots[i].data = datas.ElementAt(i);
            else
                slots[i].data = null;

            slots[i].SetUI();
        }
    }

    public void StartCooldown(float cooldownTime)
    {
        if (cooldownHandler != null) 
            return;

        cooldownHandler = StartCoroutine(Cooldown(cooldownTime));
    }

    Coroutine cooldownHandler;
    System.Collections.IEnumerator Cooldown(float cooldownTime)
    {
        isInCooldown = true;
        cooldownBar.fillAmount = 1f;

        float elapsedTime = 0f;
        while (elapsedTime < cooldownTime)
        {
            elapsedTime += Time.deltaTime;
            cooldownBar.fillAmount = 1f - elapsedTime / cooldownTime;
            yield return null;
        }

        isInCooldown = false;
        cooldownBar.fillAmount = 0f;

        cooldownHandler = null;
    }
}