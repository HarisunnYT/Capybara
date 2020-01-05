using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnclosureSpawner : MonoBehaviour
{
    public static EnclosureSpawner instance;

    [SerializeField]
    private Transform parent;

    public int spawnCount;

    [SerializeField]
    private SpawnObject[] collection;

    [SerializeField]
    private int minSize, maxSize;

    [SerializeField]
    private int xWidth, zWidth;

    [SerializeField]
    private LayerMask conflictLayer;

    void Awake()
    {
        instance = this;
    }

    public void SpawnEnclosure()
    {
        int index = Random.Range(0, collection.Length);

        xWidth = Random.Range(minSize, maxSize);
        zWidth = Random.Range(minSize, maxSize);

        Vector3 origin = new Vector3(Random.Range(-WorldGenerator.instance.mapSize, WorldGenerator.instance.mapSize), 0, Random.Range(-WorldGenerator.instance.mapSize, WorldGenerator.instance.mapSize));

        if(Physics.OverlapSphere(origin, maxSize * 2, conflictLayer).Length > 0)
        {
            SpawnEnclosure();
        }
        else
        {
            Vector3 pos = origin;
            for (int i = 0; i < xWidth; i++)
            {
                pos = new Vector3(pos.x + collection[index].bounds, pos.y, pos.z);
                SpawnFence(collection[index].gameObject, pos, Quaternion.Euler(0, 0, 0));
            }

            pos = new Vector3(pos.x + (collection[index].bounds / 2), pos.y, pos.z + (collection[index].bounds / 2));
            for (int i = 0; i < zWidth; i++)
            {
                SpawnFence(collection[index].gameObject, pos, Quaternion.Euler(0, -90, 0));
                pos = new Vector3(pos.x, pos.y, pos.z + collection[index].bounds);
            }

            pos = new Vector3(pos.x - (collection[index].bounds / 2), pos.y, pos.z - (collection[index].bounds / 2));
            for (int i = 0; i < xWidth; i++)
            {
                SpawnFence(collection[index].gameObject, pos, Quaternion.Euler(0, 180, 0));
                pos = new Vector3(pos.x - collection[index].bounds, pos.y, pos.z);
            }

            pos = new Vector3(pos.x + (collection[index].bounds / 2), pos.y, pos.z - (collection[index].bounds / 2));
            for (int i = 0; i < zWidth; i++)
            {
                SpawnFence(collection[index].gameObject, pos, Quaternion.Euler(0, 90, 0));
                pos = new Vector3(pos.x, pos.y, pos.z - collection[index].bounds);
            }
        }     
    }

    private void SpawnFence(GameObject obj, Vector3 pos, Quaternion rot)
    {
        GameObject fence = Instantiate(obj, pos, rot, parent);
        SpawnLibrary.instance.spawnedFences.Add(fence.GetComponent<SpawnObject>());
    }
}
