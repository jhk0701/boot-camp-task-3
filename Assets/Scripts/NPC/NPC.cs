using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum NpcState
{
    Idle,
    Wander,
    Attack
}

[RequireComponent(typeof(NavMeshAgent))]
public class NPC : MonoBehaviour, IDamagable
{
    [HideInInspector] public NavMeshAgent agent;

    IAgentState curState;
    public IAgentState IdleState {get; private set;}
    public IAgentState WanderState {get; private set;}
    public IAgentState TrackState {get; private set;}
    public IAgentState AttackState {get; private set;}

    public float health = 50f;
    public float damage = 5f;
    public float detectDistance = 10f;
    public float attackDistance = 2f;


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();

        IdleState = new IdleState(this);
        WanderState = new WanderState(this);
        TrackState = new TrackState(this);
        AttackState = new AttackState(this);

        ChangeState(IdleState);
    }

    
    void Update()
    {
        curState.Update();
    }


    public void TakeDamage(float amount)
    {
        health -= amount;
    }

    public void ChangeState(IAgentState state)
    {
        curState = state;
        curState.Start();
    }

    public Vector3 GetWanderDestination(float minDistance, float maxDistance, int maxTrial = 30)
    {
        Vector3 position = Vector3.zero;
        NavMeshHit hit = new NavMeshHit();

        int trial = 0;
        do
        {
            position = transform.position + Random.onUnitSphere * Random.Range(minDistance, maxDistance);
            if (NavMesh.SamplePosition(position, out hit, maxDistance, NavMesh.AllAreas))
                break;
        }
        while(trial < maxTrial);

        return hit.position;
    }    

}
