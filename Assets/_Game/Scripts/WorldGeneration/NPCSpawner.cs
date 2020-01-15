using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSpawner : Singleton<NPCSpawner>
{
    public void SpawnNPCs(NPCManager.SpawnGroup spawnGroup)
    {
        for (int i = 0; i < spawnGroup.spawnCount; i++)
        {
            PlaceNPC(spawnGroup, WorldQuadrants.Instance.GetQuadrant(i, spawnGroup.spawnCount));
        }
    }

    private void PlaceNPC(NPCManager.SpawnGroup spawnGroup, int quadrantIndex)
    {
        int index = Random.Range(0, spawnGroup.collection.Length);
        Quaternion rot = Quaternion.Euler(0, Random.Range(0, 360), 0);

        Vector3 pos = WorldQuadrants.Instance.GetSpawnPosInQuadrant(quadrantIndex, spawnGroup.collection[index].MaxBounds(), spawnGroup.conflictLayer);

        Instantiate(spawnGroup.collection[index].gameObject, pos, rot, spawnGroup.parent);
    }
}
