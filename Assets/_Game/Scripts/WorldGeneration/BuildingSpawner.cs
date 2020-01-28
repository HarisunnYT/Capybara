using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSpawner : Singleton<BuildingSpawner>
{
    public Transform parent;

    [SerializeField]
    private int spawnCount = 4; // 4, 8, 12, 16, ...

    [SerializeField]
    private SpawnObject[] collection;

    [SerializeField]
    private LayerMask conflictLayer;

    private int[] rotations = { 0, 90, 180, 270 };

    public bool InitSpawnBuildings()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            PickBuilding(WorldQuadrants.Instance.GetQuadrant(i, spawnCount));
        }

        return true;
    }

    private bool PickBuilding(int quadrantIndex)
    {
        int index = Random.Range(0, collection.Length);
        Quaternion rot = Quaternion.Euler(-90, rotations[Random.Range(0, rotations.Length)], 0);
        Vector3 pos = WorldQuadrants.Instance.GetSpawnPosInQuadrant(quadrantIndex, collection[index].MaxBounds(), conflictLayer);

        GameObject obj = Instantiate(collection[index].gameObject, pos, rot, parent);
        SetBuildingNodesUsed(pos, collection[index]);

        return true;
    }

    public void SpawnBuildingAtPos(Vector3 pos, Quaternion rot)
    {
        int index = Random.Range(0, collection.Length);

        Instantiate(collection[index].gameObject, pos, rot, parent);
        SetBuildingNodesUsed(pos, collection[index]);
    }

    private void SetBuildingNodesUsed(Vector3 pos, SpawnObject spawnObject)
    {
        NodeManager.Instance.SetObjectNodesUsed(new Vector3(pos.x - (spawnObject.MaxBounds() / 2), 0, pos.z - (spawnObject.MaxBounds() / 2)), Mathf.RoundToInt(spawnObject.MaxBounds()), Mathf.RoundToInt(spawnObject.MaxBounds()), 1);
    }
}
