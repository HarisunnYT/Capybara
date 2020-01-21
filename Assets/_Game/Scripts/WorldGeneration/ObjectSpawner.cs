using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : Singleton<ObjectSpawner>
{
    public void SpawnObjects(ObjectManager.SpawnGroup spawnGroup)
    {
        for (int i = 0; i < spawnGroup.spawnCount; i++)
        {
            PlaceItem(spawnGroup, WorldQuadrants.Instance.GetQuadrant(i, spawnGroup.spawnCount));
        }
    }

    private void PlaceItem(ObjectManager.SpawnGroup spawnGroup, int quadrantIndex)
    {
        int index = Random.Range(0, spawnGroup.collection.Length);
        Quaternion rot = Quaternion.Euler(0, Random.Range(0, 360), 0);

        Vector3 pos = WorldQuadrants.Instance.GetSpawnPosInQuadrant(quadrantIndex, spawnGroup.collection[index].MaxBounds(), spawnGroup.conflictLayer);

        Instantiate(spawnGroup.collection[index].gameObject, new Vector3(pos.x, spawnGroup.collection[index].bounds.y, pos.z), rot, spawnGroup.parent);
        NodeManager.Instance.SetNodeUsed(pos);
    }
}
