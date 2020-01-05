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

        Node node = NodeManager.instance.nodes[Random.Range(0, NodeManager.instance.nodes.Count)];
        Vector3 origin = node.pos;

        if(Physics.OverlapSphere(origin, maxSize * 2, conflictLayer).Length > 0 || node.used)
        {
            SpawnEnclosure();
        }
        else
        {
            Vector3 pos = origin;
            for (int i = 0; i < xWidth; i++)
            {
                pos = new Vector3(pos.x + collection[index].bounds.x, pos.y, pos.z);
                SpawnFence(index, false, collection[index].gameObject, pos, Quaternion.Euler(0, 0, 0));            
            }

            //rotated
            pos = new Vector3(pos.x + (collection[index].GetRotatedBounds().x / 2), pos.y, pos.z + (collection[index].GetRotatedBounds().z / 2));
            for (int i = 0; i < zWidth; i++)
            {
                SpawnFence(index, true, collection[index].gameObject, pos, Quaternion.Euler(0, -90, 0));
                pos = new Vector3(pos.x, pos.y, pos.z + collection[index].GetRotatedBounds().z);
            }

            pos = new Vector3(pos.x - (collection[index].bounds.x / 2), pos.y, pos.z - (collection[index].bounds.z / 2));
            for (int i = 0; i < xWidth; i++)
            {
                SpawnFence(index, false, collection[index].gameObject, pos, Quaternion.Euler(0, 180, 0));
                pos = new Vector3(pos.x - collection[index].bounds.x, pos.y, pos.z);
            }

            //rotated
            pos = new Vector3(pos.x + (collection[index].GetRotatedBounds().x / 2), pos.y, pos.z - (collection[index].GetRotatedBounds().z / 2));
            for (int i = 0; i < zWidth; i++)
            {
                SpawnFence(index, true, collection[index].gameObject, pos, Quaternion.Euler(0, 90, 0));
                pos = new Vector3(pos.x, pos.y, pos.z - collection[index].GetRotatedBounds().z);
            }
        }     
    }

    private void SpawnFence(int index, bool zAxis, GameObject obj, Vector3 pos, Quaternion rot)
    {
        GameObject fence = Instantiate(obj, pos, rot, parent);
        SpawnLibrary.instance.spawnedFences.Add(fence.GetComponent<SpawnObject>());

        List<Node> nodes = zAxis ? NodeManager.instance.GetNodesInRange(new Vector3(pos.x, 0, pos.z - collection[index].GetRotatedBounds().z / 2), new Vector3(pos.x + collection[index].GetRotatedBounds().x / 2, 0, pos.z + collection[index].GetRotatedBounds().z)) :
            NodeManager.instance.GetNodesInRange(new Vector3(pos.x - collection[index].bounds.x / 2, 0, pos.z), new Vector3(pos.x + collection[index].bounds.x / 2, 0, pos.z));
            
        foreach (Node nodeInList in nodes)
        {
            if(nodeInList != null)
            {
                nodeInList.used = true;
            }         
        }
    }
}
