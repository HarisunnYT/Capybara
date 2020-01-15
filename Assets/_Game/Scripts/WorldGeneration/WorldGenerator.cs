using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldGenerator : Singleton<WorldGenerator>
{
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

    protected override void Initialize()
    {
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

        WorldQuadrants.Instance.SetupQuadrants();

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
        EnclosureManager.Instance.InitEnclosureSpawn();
        BuildingSpawner.Instance.InitSpawnBuildings();
        NPCManager.Instance.InitSpawnNPCs();
        ObjectManager.Instance.InitSpawnObjects();
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
