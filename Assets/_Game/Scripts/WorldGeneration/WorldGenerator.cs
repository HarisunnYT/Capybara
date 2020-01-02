using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public static WorldGenerator instance;

    public int mapSize;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        PathSpawner.instance.SpawnPath();
        GridSpawner.instance.SpawnGrid();

        for (int i = 0; i < EnclosureSpawner.instance.spawnCount; i++)
        {
            EnclosureSpawner.instance.SpawnEnclosure();
        }
        
        Invoke("DelayedObjectSpawn", .5f);
    }

    private void DelayedObjectSpawn()
    {
        for (int i = 0; i < ObjectSpawner.instance.spawnCount; i++)
        {
            ObjectSpawner.instance.SpreadItem();
        }

        Invoke("DelayedEnemySpawn", .5f);
    }

    private void DelayedEnemySpawn()
    {
        for (int i = 0; i < EnemySpawner.instance.spawnCount; i++)
        {
            EnemySpawner.instance.SpreadEnemy();
        }
    }
}
