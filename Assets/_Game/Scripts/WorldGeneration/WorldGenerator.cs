using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    public static WorldGenerator instance;

    [SerializeField]
    private GameObject loadingCanvas;

    public int mapSize;

    private void Awake()
    {
        instance = this;
        loadingCanvas.SetActive(true);
    }

    void Start()
    {
        Invoke("DelayedLoadIn", .2f);
    }

    private void DelayedLoadIn()
    {
        GridSpawner.instance.SpawnGrid();

        for (int i = 0; i < EnclosureSpawner.instance.spawnCount; i++)
        {
            EnclosureSpawner.instance.SpawnEnclosure();
        }

        Invoke("DelayedObjectSpawn", .2f);
    }

    private void DelayedObjectSpawn()
    {
        for (int i = 0; i < ObjectSpawner.instance.spawnCount; i++)
        {
            ObjectSpawner.instance.SpreadItem();
        }

        Invoke("DelayedEnemySpawn", .2f);
    }

    private void DelayedEnemySpawn()
    {
        for (int i = 0; i < EnemySpawner.instance.spawnCount; i++)
        {
            EnemySpawner.instance.SpreadEnemy();
        }

        Invoke("DelayedPathSpawn", .2f);
    }

    private void DelayedPathSpawn()
    {
        PathSpawner.instance.AssignPathNodes();
    }

    public void CompletedGeneration()
    {
        loadingCanvas.SetActive(false);
    }
}
