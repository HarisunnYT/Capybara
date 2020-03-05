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
    public int segmentSize;

    public Spawner spawner;

    [SerializeField]
    private GameObject player;
    public GameObject spawnedPlayer;

    [SerializeField]
    private GameObject debugCanvas;

    [SerializeField]
    private Transform ground;

    [SerializeField]
    private TextMeshProUGUI seed, genTime;

    public bool treasureRoomSpawned = false, bossRoomSpawned = false;

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
        if (isDebug)
        {
            yield return new WaitForSeconds(.25f);
        }

        yield return null;

        yield return new WaitUntil(() => EnclosureManager.Instance.InitEnclosureSpawn());

        if (generatePaths)
        {
            PathGenerator.Instance.DrawPath();
            yield return new WaitUntil(() => PathGenerator.Instance.CompletedPathSpawns());
        }

        //yield return new WaitUntil(() => ObjectManager.Instance.InitSpawnObjects());
        
        CompletedGeneration();
    }

    public void CompletedGeneration()
    {
        ObjectManager.Instance.InitSpawnObjects();

        genComplete = true;

        if (player != null)
        {
            spawnedPlayer = Instantiate(player, NodeManager.Instance.GetRandomUnusedNode().pos, Quaternion.identity);
            Camera.main.gameObject.SetActive(false);
        }
        else
        {
            Camera.main.gameObject.SetActive(true);
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
