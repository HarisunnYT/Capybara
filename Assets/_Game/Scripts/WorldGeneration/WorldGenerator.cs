using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldGenerator : MonoBehaviour
{
    public static WorldGenerator instance;

    [SerializeField]
    private bool isDebug;

    private bool genComplete = false;

    [SerializeField]
    private GameObject loadingCanvas;

    public int mapSize;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject debugCanvas;

    [SerializeField]
    private Transform ground;

    [SerializeField]
    private TextMeshProUGUI seed, genTime;

    private void Awake()
    {
        instance = this;
        loadingCanvas.SetActive(true);
    }

    void Start()
    {
        debugCanvas.SetActive(isDebug);

        if (isDebug)
        {           
            seed.text = WorldSeed.instance.seed.ToString();
        }

        ground.position = new Vector3(mapSize / 2, 0, mapSize / 2);
        ground.localScale = new Vector3(mapSize / 9, 1, mapSize / 9);
        
        Invoke("DelayedLoadIn", .1f);
    }

    private void Update()
    {
        if (isDebug && !genComplete)
        {
            genTime.text = "Gen time: " + Time.timeSinceLevelLoad.ToString("F2") + "s";
        }
    }

    private void DelayedLoadIn()
    {
        PathGenerator.Instance.AddCentralAreaToPathDest();

        for (int i = 0; i < EnclosureSpawner.Instance.spawnCount; i++)
        {
            EnclosureManager.Instance.EnclosureSpawn(i, mapSize);
        }

        DelayedObjectSpawn();
    }

    private void DelayedObjectSpawn()
    {
        BuildingSpawner.Instance.SpawnBuildings();
        ObjectSpawner.Instance.SpawnObjects();

        DelayedEnemySpawn();
    }

    private void DelayedEnemySpawn()
    {
        for (int i = 0; i < EnemySpawner.Instance.spawnCount; i++)
        {
            EnemySpawner.Instance.SpreadEnemy();
        }

        DelayedPathSpawn();
    }

    private void DelayedPathSpawn()
    {
        PathGenerator.Instance.DrawPath();
    }

    public void CompletedGeneration()
    {
        genComplete = true;

        if (player != null)
        {
            Instantiate(player, NodeManager.Instance.GetRandomUnusedNode().pos, Quaternion.identity);
        }

        loadingCanvas.SetActive(false);
    }

    public void RegenerateWorld()
    {
        if (isDebug)
        {
            Application.LoadLevel(Application.loadedLevel);
        }       
    }
}
