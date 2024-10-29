using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class NpcManager : Singleton<NpcManager>
{
    [SerializeField] GameObject enemy;
    [SerializeField] int spawnCount = 10;
    [SerializeField] Transform[] spawnPoints;

    public Transform target;
    [SerializeField] NavMeshSurface navMeshSurface;


    // Start is called before the first frame update
    void Start()
    {
        Build();

        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 pos = spawnPoints[i % spawnPoints.Length].position;
            GameObject inst = Instantiate(enemy, pos, Quaternion.identity);
            inst.SetActive(true);
        }
    }

    [ContextMenu("Build")]
    public void Build()
    {
        navMeshSurface.BuildNavMesh();
    }

}
