using UnityEngine;
using UnityEngine.AI;

public class WanderState : IAgentState
{
    
    public NPC Npc { get; set; }

    float maxWanderDistance = 10f;
    float minWanderDistance = 2f;
    float stopDistance = 0.1f;

    float checkRate = 0.2f;
    float time;

    public WanderState(NPC npc)
    {
        Npc = npc;
    }


    public void Start()
    {
        Npc.agent.isStopped = false;

        Vector3 targetPosition = Npc.GetWanderDestination(minWanderDistance, maxWanderDistance);
        Npc.agent.SetDestination(targetPosition);

        time = Time.time;
    }

    public void Update()
    {
        if (Time.time - time > checkRate)
        {
            Check();
        }
    }

    void Check()
    {
        if (NpcManager.Instance.target != null && 
            Vector3.Distance(Npc.transform.position, NpcManager.Instance.target.position) < Npc.detectDistance)
        {
            Npc.ChangeState(Npc.TrackState);
            return;
        }

        if (Npc.agent.remainingDistance < stopDistance)
        {
            Npc.ChangeState(Npc.IdleState);
        }
        
    }
}
