using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public static ObjectSpawner instance;

    public Transform parent;

    public int spawnCount = 5;

    [SerializeField]
    private SpawnObject[] collection;

    [SerializeField]
    private LayerMask conflictLayer;

    void Awake()
    {
        instance = this;      
    }

    public void SpreadItem()
    {
        Vector3 pos = NodeManager.instance.GetRandomUnusedNode().pos;
        PickObject(pos);
    }

    private void PickObject(Vector3 pos)
    {
        int index = Random.Range(0, collection.Length);
        Quaternion rot = Quaternion.Euler(0, Random.Range(0, 360), 0);
        SpawnObject spawnObject = collection[index];

        // check for object overlap
        if (Physics.OverlapBox(pos, new Vector3(spawnObject.MaxBounds(), Mathf.Infinity, spawnObject.MaxBounds()), rot, conflictLayer).Length > 0)
        {
            Debug.Log("Colliders found, rerunning spawn for object");
            SpreadItem();
        }
        else
        {
            GameObject obj = Instantiate(collection[index].gameObject, pos, rot, parent);
            SpawnLibrary.instance.spawnedObjects.Add(obj.GetComponent<SpawnObject>());
        }
    }
}
