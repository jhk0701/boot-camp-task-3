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
        PlayerManager.Instance.player.consumableItems = this;
        
        // TODO : 이런 방식의 접근이 많아지면 player에 캐싱해두거나 다른 방법을 생각해봐야 할 듯.
        PlayerManager.Instance.player.GetComponent<PlayerController>().OnUseItemEvent += UseItem;
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

        // 맨 처음 item을 사용
        ItemData data = datas.Dequeue();
        PlayerManager.Instance.player.StartItemEffect(data);
        
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