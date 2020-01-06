using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField]
    private bool isDebug;

    public static WorldGenerator instance;

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
    private TextMeshProUGUI seed;

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
        ground.localScale = new Vector3(mapSize / 10, 1, mapSize / 10);
        
        Invoke("DelayedLoadIn", .2f);
    }

    private void DelayedLoadIn()
    {
        PathSpawner.instance.AddCentralAreaToPathDest();

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
        if (player != null)
        {
            Instantiate(player, NodeManager.instance.GetRandomUnusedNode().pos, Quaternion.identity);
        }

        loadingCanvas.SetActive(false);
    }

    public void RegenerateWorld()
    {
        if (isDebug)
        {
            Application.LoadLevel(Application.loadedLevel);

            //foreach (Transform child in EnclosureSpawner.instance.parent)
            //{
            //    Destroy(child.gameObject);
            //}

            //foreach (Transform child in PathSpawner.instance.parent)
            //{
            //    Destroy(child.gameObject);
            //}

            //foreach (Transform child in ObjectSpawner.instance.parent)
            //{
            //    Destroy(child.gameObject);
            //}

            //foreach (Transform child in EnemySpawner.instance.parent)
            //{
            //    Destroy(child.gameObject);
            //}


            //loadingCanvas.SetActive(true);

            //WorldSeed.instance.SetSeed(true);
            //seed.text = WorldSeed.instance.seed.ToString();

            //Invoke("DelayedLoadIn", .2f);
        }       
    }
}
