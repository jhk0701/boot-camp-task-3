using UnityEngine;
using UnityEngine.AI;

public class TrackState : IAgentState
{
    public NPC Npc { get; set; }
    Transform target => NpcManager.Instance.target;

    float targetDistance;
    float refreshRate = 0.2f;
    float lastRefreshTime;

    public TrackState(NPC npc)
    {
        Npc = npc;
    }

    public void Start()
    {
        Refresh();
    }

    public void Update()
    {
        if (Time.time - lastRefreshTime > refreshRate)
        {
            Refresh();
        }
    }

    void Refresh()
    {
        targetDistance = Vector3.Distance(Npc.transform.position, target.position);
        lastRefreshTime = Time.time;

        if (targetDistance <= Npc.attackDistance)
        {
            // 공격
            // Attack();
            
        }
        else if (targetDistance <= Npc.detectDistance)
        {
            // 추격
            NavMeshPath path = new NavMeshPath();
            if (Npc.agent.CalculatePath(target.position, path))
            {
                Npc.agent.isStopped = false;
                Npc.agent.SetDestination(target.position); 
            }
            else
            {
                Npc.ChangeState(Npc.IdleState);
            }
        }
        else if(targetDistance > Npc.detectDistance)
        {
            Npc.ChangeState(Npc.IdleState);
        }
    }

}
