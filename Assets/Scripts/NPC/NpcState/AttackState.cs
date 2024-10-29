using UnityEngine;

public class AttackState : IAgentState
{
    public NPC Npc { get; set; }

    public AttackState(NPC npc)
    {
        Npc = npc;
    }

    public void Start()
    {
    }

    public void Update()
    {
    }
}