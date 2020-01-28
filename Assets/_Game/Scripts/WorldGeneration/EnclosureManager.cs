using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnclosureManager : Singleton<EnclosureManager>
{
    public List<SpawnObject> enclosures;

    public int spawnCount;

    [SerializeField]
    private int zooBorder = 5;

    [SerializeField]
    private LayerMask conflictLayer;

    public bool InitEnclosureSpawn()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            EnclosureSpawn(WorldQuadrants.Instance.GetQuadrant(i, spawnCount), WorldGenerator.Instance.mapSize);
        }

        return true;
    }

    private void EnclosureSpawn(int quadrantIndex, int mapSize)
    {
        Vector3 pos = WorldQuadrants.Instance.GetSpawnPosInQuadrant(quadrantIndex, EnclosureSpawner.Instance.maxFencesPerSide, conflictLayer);

        // OLD // EnclosureSpawner.Instance.SpawnEnclosure(node);
        EnclosureSpawner.Instance.SpawnPrefabbedEnclosure(pos);
    }

    public SpawnObject GetRandomEnclosureBySize(int xLength, int zLength)
    {
        string size = xLength.ToString() + "x" + zLength.ToString();

        SpawnObject enclosure = enclosures.Find(x => x.name.Contains(size));

        return enclosure;
    }
}
