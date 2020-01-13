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

    private int[] rotations = { 0, 90, 180, 270 };

    public void SpawnBuildings()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            PickBuilding();
        }
    }

    private bool PickBuilding()
    {
        int index = Random.Range(0, collection.Length);
        SpawnObject spawnObject = collection[index];

        Quaternion rot = Quaternion.Euler(-90, rotations[Random.Range(0, rotations.Length)], 0);
        Vector3 pos = NodeManager.Instance.GetRandomUnusedNode().pos;

        while (Physics.OverlapBox(pos, new Vector3(spawnObject.Barrier(), 0, spawnObject.Barrier()), Quaternion.identity, conflictLayer).Length > 0)
        {
            Debug.Log("Building collision");
            pos = NodeManager.Instance.GetRandomUnusedNode().pos;
        }

        GameObject obj = Instantiate(collection[index].gameObject, pos, rot, parent);
        NodeManager.Instance.SetObjectNodesUsed(new Vector3(pos.x - (spawnObject.MaxBounds() / 2), 0, pos.z - (spawnObject.MaxBounds() / 2)), Mathf.RoundToInt(spawnObject.MaxBounds()), Mathf.RoundToInt(spawnObject.MaxBounds()), 1);

        return true;
    }
}
