using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : Singleton<BuildingSpawner>
{
    public Transform parent;

    [SerializeField]
    private int spawnCount = 3;

    [SerializeField]
    private SpawnObject[] collection;

    [SerializeField]
    private LayerMask conflictLayer;

    public void SpawnBuildings()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            SpreadBuilding();
        }
    }

    private void SpreadBuilding()
    {
        Vector3 pos = NodeManager.Instance.GetRandomUnusedNode().pos;
        PickBuilding(pos);
    }

    private void PickBuilding(Vector3 pos)
    {
        int index = Random.Range(0, collection.Length);
        Quaternion rot = Quaternion.Euler(-90, Random.Range(0, 360), 0);
        SpawnObject spawnObject = collection[index];

        // check for object overlap
        if (Physics.OverlapBox(pos, new Vector3(spawnObject.MaxBounds(), Mathf.Infinity, spawnObject.MaxBounds()), rot, conflictLayer).Length > 0)
        {
            Debug.Log("Colliders found, rerunning spawn for building");
            SpreadBuilding();
        }
        else
        {
            GameObject obj = Instantiate(collection[index].gameObject, pos, rot, parent);
        }
    }
}
