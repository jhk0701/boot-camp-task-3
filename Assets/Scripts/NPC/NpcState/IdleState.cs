using UnityEngine;

public class IdleState : IAgentState
{
    public NPC Npc { get; set; }

    float maxDuration = 10f;
    float minDuraiton = 3f;
    float duration = 0f;
    float time = 0f;

    public IdleState(NPC npc)
    {
        Npc = npc;
    }


    public void Start()
    {
        Npc.agent.isStopped = true;
        Npc.agent.SetDestination(Npc.transform.position);

        duration = Random.Range(minDuraiton, maxDuration);
        time = Time.time;
    }

    public void Update()
    {
        if(Time.time - time > duration)
        {
            Npc.ChangeState(Npc.WanderState);
        }
    }
}
