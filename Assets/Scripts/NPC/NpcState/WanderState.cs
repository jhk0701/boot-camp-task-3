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

        Vector3 targetPosition = GetWanderDestination(minWanderDistance, maxWanderDistance);
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
        else
        {
            Vector3 destination = Npc.agent.destination;
            NavMeshPath path = new NavMeshPath();
            if (Npc.agent.CalculatePath(destination, path))
            {
                Npc.agent.SetDestination(destination); 
            }
            else // 이동 불가 시, 새로운 곳으로
            {
                destination = GetWanderDestination(minWanderDistance, maxWanderDistance);
                Npc.agent.SetDestination(destination);
            }
        }
    }

    
    public Vector3 GetWanderDestination(float minDistance, float maxDistance, int maxTrial = 30)
    {
        Vector3 position = Vector3.zero;
        NavMeshHit hit = new NavMeshHit();

        int trial = 0;
        do
        {
            position = Npc.transform.position + Random.onUnitSphere * Random.Range(minDistance, maxDistance);
            if (NavMesh.SamplePosition(position, out hit, maxDistance, NavMesh.AllAreas))
                break;
        }
        while(trial < maxTrial);

        return hit.position;
    }   
}
