using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralAreaSpawner : Singleton<CentralAreaSpawner>
{
    public Transform parent;

    [SerializeField]
    private SpawnObject[] collection;

    public void SpawnCentralAreaAtPos(Vector3 pos)
    {
        int index = Random.Range(0, collection.Length);

        Instantiate(collection[index].gameObject, pos, Quaternion.identity, parent);
        SetNodesInAreaUsed(pos, collection[index]);
    }

    private void SetNodesInAreaUsed(Vector3 pos, SpawnObject spawnObject)
    {
        NodeManager.Instance.SetObjectNodesUsed(new Vector3(pos.x - (spawnObject.MaxBounds() / 2), 0, pos.z - (spawnObject.MaxBounds() / 2)), Mathf.RoundToInt(spawnObject.MaxBounds()), Mathf.RoundToInt(spawnObject.MaxBounds()), 1);
    }
}
