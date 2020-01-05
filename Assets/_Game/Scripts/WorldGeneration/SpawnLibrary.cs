using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLibrary : MonoBehaviour
{
    public static SpawnLibrary instance = null;

    public List<SpawnObject> spawnedObjects = new List<SpawnObject>();
    public List<SpawnObject> spawnedTiles = new List<SpawnObject>();
    public List<SpawnObject> spawnedFences = new List<SpawnObject>();
    public List<SpawnObject> spawnedPaths = new List<SpawnObject>();
    public List<SpawnObject> spawnedEnemies = new List<SpawnObject>();

    private void Awake()
    {
        instance = this;
    }
}
