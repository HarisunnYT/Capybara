using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnclosureSpawner : Singleton<EnclosureSpawner>
{
    private const int enclosureGroupSpawnChance = 80;

    public void SpawnPrefabbedEnclosures()
    {
        for (int x = WorldGenerator.Instance.segmentSize; x < WorldGenerator.Instance.mapSize; x += WorldGenerator.Instance.segmentSize)
        {
            for (int z = WorldGenerator.Instance.segmentSize; z < WorldGenerator.Instance.mapSize; z += WorldGenerator.Instance.segmentSize)
            {
                Spawner spawnedSpawner = Instantiate(WorldGenerator.Instance.spawner, new Vector3(x, 0, z), Quaternion.identity);
                spawnedSpawner.spawnType = Spawner.SpawnType.EnclosureGroup;
                spawnedSpawner.spawnChance = 80;
            }
        }
    }
}
