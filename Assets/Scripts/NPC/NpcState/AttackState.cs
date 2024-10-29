using UnityEngine;

public class AttackState : IAgentState
{
    public NPC Npc { get; set; }
    Transform target => NpcManager.Instance.target;
    
    float attackRate = 1f;
    float lastAttackTime;

    public AttackState(NPC npc)
    {
        Npc = npc;
    }

    public void Start()
    {
        lastAttackTime = 0f;
    }

    public void Update()
    {
        if (Time.time - lastAttackTime > attackRate)
        {
            TryAttack();
        }
    }

    void TryAttack()
    {
        lastAttackTime = Time.time;
        float distance = Vector3.Distance(Npc.transform.position, target.position);
        if (distance <= Npc.attackDistance)
        {
            if(target.TryGetComponent(out IDamagable damagable))
            {
                damagable.TakeDamage(Npc.damage);
            }
        }
        else
        {
            Npc.ChangeState(Npc.TrackState);
        }
    }
}