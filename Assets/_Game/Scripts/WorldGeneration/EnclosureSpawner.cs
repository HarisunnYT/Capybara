using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnclosureSpawner : Singleton<EnclosureSpawner>
{
    public Transform parent;

    public int spawnCount;

    [SerializeField]
    private SpawnObject[] collection;

    [SerializeField]
    private int minFencesPerSide, maxFencesPerSide;

    public int zooBorder;

    private int xWidth, zWidth;

    private List<Node> enclosurePathNodes = new List<Node>();

    [SerializeField]
    private LayerMask conflictLayer;

    public void SpawnEnclosure(Node node)
    {      
        int index = Random.Range(0, collection.Length);
        SpawnObject fencePiece = collection[index];

        xWidth = Random.Range(minFencesPerSide, maxFencesPerSide);
        zWidth = Random.Range(minFencesPerSide, maxFencesPerSide);

        Vector3 initPos = node.pos;

        enclosurePathNodes.Clear();

        Vector3 pos = initPos;
        for (int i = 0; i < xWidth; i++)
        {                             
            SpawnFenceOnX(fencePiece, pos, Quaternion.Euler(0, 0, 0));               
            pos = new Vector3(pos.x + fencePiece.bounds.x, pos.y, pos.z);
        }

        for (int i = 0; i < zWidth; i++)
        {
            SpawnFenceOnZ(fencePiece, pos, Quaternion.Euler(0, -90, 0));
            pos = new Vector3(pos.x, pos.y, pos.z + fencePiece.GetRotatedBounds().z);
        }

        pos = new Vector3(pos.x - fencePiece.bounds.x, pos.y, pos.z);
        for (int i = 0; i < xWidth; i++)
        {
            SpawnFenceOnX(fencePiece, pos, Quaternion.Euler(0, 180, 0));
            pos = new Vector3(pos.x - fencePiece.bounds.x, pos.y, pos.z);
        }

        pos = new Vector3(pos.x + fencePiece.bounds.x, pos.y, pos.z - fencePiece.GetRotatedBounds().z);
        for (int i = 0; i < zWidth; i++)
        {
            SpawnFenceOnZ(fencePiece, pos, Quaternion.Euler(0, -90, 0));                
            pos = new Vector3(pos.x, pos.y, pos.z - fencePiece.GetRotatedBounds().z);
        }
        SetEnclosureNodes(initPos, fencePiece);

        Node pathDest = enclosurePathNodes[Random.Range(0, enclosurePathNodes.Count)];
        if (pathDest != null && (pathDest.enclosure || pathDest.used))
        {
            pathDest.used = false;
            pathDest.enclosure = false;
        }
        NodeManager.Instance.pathDests.Add(pathDest);
            
    }

    private void SpawnFenceOnX(SpawnObject obj, Vector3 pos, Quaternion rot)
    {
        GameObject fence = Instantiate(obj.gameObject, new Vector3((pos.x + obj.bounds.x + pos.x) / 2, pos.y, pos.z), rot, parent);

        List<Node> nodes = new List<Node>();
        for (int i = 0; i < obj.bounds.x; i++)
        {
            Node node = NodeManager.Instance.GetNodeAtPosition(new Vector3(pos.x + i, 0, pos.z));
            nodes.Add(node);
            enclosurePathNodes.Add(node);
        }

        NodeManager.Instance.SetExistingNodesUsed(nodes);
    }

    private void SpawnFenceOnZ(SpawnObject obj, Vector3 pos, Quaternion rot)
    {
        GameObject fence = Instantiate(obj.gameObject, new Vector3(pos.x, pos.y, (pos.z + obj.GetRotatedBounds().z + pos.z) / 2), rot, parent);

        List<Node> nodes = new List<Node>();
        for (int i = 0; i < obj.GetRotatedBounds().z; i++)
        {
            Node node = NodeManager.Instance.GetNodeAtPosition(new Vector3(pos.x, 0, pos.z + i));
            nodes.Add(node);
            enclosurePathNodes.Add(node);
        }
        NodeManager.Instance.SetExistingNodesUsed(nodes);
    }

    private void SetEnclosureNodes(Vector3 pos, SpawnObject obj)
    {
        List<Vector3> enclosureNodes = new List<Vector3>();

        for (int i = 0; i < xWidth * obj.bounds.x; i++)
        {
            for (int z = 0; z < zWidth * obj.GetRotatedBounds().z; z++)
            {
                enclosureNodes.Add(new Vector3(pos.x + i, pos.y, pos.z + z));
            }
        }      
        NodeManager.Instance.SetEnclosure(enclosureNodes);
    }
}
