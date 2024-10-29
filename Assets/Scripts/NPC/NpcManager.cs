using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class NpcManager : Singleton<NpcManager>
{
    [SerializeField] GameObject enemy;
    [SerializeField] int spawnCount = 10;

    public Transform target;
    [SerializeField] NavMeshSurface[] navMeshSurfaces;


    // Start is called before the first frame update
    void Start()
    {
        Build();

        for (int i = 0; i < spawnCount; i++)
        {
            GameObject inst = Instantiate(enemy);
            inst.SetActive(true);
        }
    }

    [ContextMenu("Build")]
    public void Build()
    {
        for (int i = 0; i < navMeshSurfaces.Length; i++)
        {
            navMeshSurfaces[i].BuildNavMesh();
        }
    }

}
