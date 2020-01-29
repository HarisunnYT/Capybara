using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WorldGenerator : Singleton<WorldGenerator>
{
    [SerializeField]
    private bool isDebug, generatePaths;

    private bool genComplete = false;

    [SerializeField]
    private GameObject loadingCanvas;

    public int mapSize;

    [SerializeField]
    private GameObject player;
    public GameObject spawnedPlayer;

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

        StartCoroutine(DelayedLoadIn());
    }

    private void Update()
    {
        if (isDebug && !genComplete)
        {
            genTime.text = "Gen time: " + Time.timeSinceLevelLoad.ToString("F2") + "s";
        }

        if (isDebug && Input.GetKeyDown(KeyCode.R))
        {
            RegenerateWorld();
        }
    }

    private IEnumerator DelayedLoadIn()
    {
        yield return null;
        yield return new WaitUntil(() => PathGenerator.Instance.AddCentralAreaToPathDest());
        yield return new WaitUntil(() => EnclosureManager.Instance.InitEnclosureSpawn());
        yield return new WaitUntil(() => BuildingSpawner.Instance.InitSpawnBuildings());
        yield return new WaitUntil(() => NPCManager.Instance.InitSpawnNPCs());
        yield return new WaitUntil(() => ObjectManager.Instance.InitSpawnObjects());

        if (generatePaths)
        {
            PathGenerator.Instance.DrawPath();
        }
        else
        {
            CompletedGeneration();
        }
    }

    public void CompletedGeneration()
    {
        genComplete = true;

        if (player != null)
        {
            spawnedPlayer = Instantiate(player, NodeManager.Instance.GetRandomUnusedNode().pos, Quaternion.identity);
        }
        else
        {
            Camera.main.gameObject.SetActive(true);
        }

        ProceduralPath.Instance.CompleteProceduralPath();

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
