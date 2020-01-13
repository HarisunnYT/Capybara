using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnclosureSpawner : Singleton<EnclosureSpawner>
{
    private const int fenceUnitLength = 4;

    public Transform parent;

    public int spawnCount;

    [SerializeField]
    private SpawnObject[] collection;
  
    public int minFencesPerSide, maxFencesPerSide;

    private int xWidth, zWidth;

    private List<Node> enclosurePathNodes = new List<Node>();

    [SerializeField]
    private LayerMask conflictLayer;

    // NEW SYSTEM
    public void SpawnPrefabbedEnclosure(Vector3 pos)
    {
        xWidth = Random.Range(minFencesPerSide, maxFencesPerSide + 1);
        zWidth = Random.Range(minFencesPerSide, maxFencesPerSide + 1);

        SpawnObject enclosure = EnclosureManager.Instance.GetRandomEnclosureBySize(xWidth, zWidth);

        Instantiate(enclosure, pos, Quaternion.identity, parent);
        NodeManager.Instance.pathDests.Add(NodeManager.Instance.GetNodeAtPosition(new Vector3(pos.x, 0, (pos.z - (zWidth * fenceUnitLength)) - 1)));

        NodeManager.Instance.SetEnclosureNodes(new Vector3(pos.x - ((xWidth * fenceUnitLength) / 2), pos.y, pos.z - (zWidth * fenceUnitLength) / 2), xWidth, zWidth, fenceUnitLength);
    }

    // OLD SYSTEM
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
            SpawnFenceOnX(fencePiece, pos, Quaternion.Euler(0, 0, 0), false);               
            pos = new Vector3(pos.x + fencePiece.bounds.x, pos.y, pos.z);
        }

        for (int i = 0; i < zWidth; i++)
        {
            SpawnFenceOnZ(fencePiece, pos, Quaternion.Euler(0, -90, 0), false);
            pos = new Vector3(pos.x, pos.y, pos.z + fencePiece.GetRotatedBounds().z);
        }

        pos = new Vector3(pos.x - fencePiece.bounds.x, pos.y, pos.z);
        for (int i = 0; i < xWidth; i++)
        {
            SpawnFenceOnX(fencePiece, pos, Quaternion.Euler(0, 180, 0), true);
            pos = new Vector3(pos.x - fencePiece.bounds.x, pos.y, pos.z);
        }

        pos = new Vector3(pos.x + fencePiece.bounds.x, pos.y, pos.z - fencePiece.GetRotatedBounds().z);
        for (int i = 0; i < zWidth; i++)
        {
            SpawnFenceOnZ(fencePiece, pos, Quaternion.Euler(0, -90, 0), true);                
            pos = new Vector3(pos.x, pos.y, pos.z - fencePiece.GetRotatedBounds().z);
        }
        NodeManager.Instance.SetEnclosureNodes(initPos, xWidth, zWidth, fenceUnitLength);

        Node pathDest = enclosurePathNodes[Random.Range(0, enclosurePathNodes.Count)];
        if (pathDest != null && (pathDest.enclosure || pathDest.used))
        {
            pathDest.used = false;
            pathDest.enclosure = false;
        }
        NodeManager.Instance.pathDests.Add(pathDest);
            
    }

    private void SpawnFenceOnX(SpawnObject obj, Vector3 pos, Quaternion rot, bool top)
    {
        GameObject fence = Instantiate(obj.gameObject, new Vector3((pos.x + obj.bounds.x + pos.x) / 2, pos.y, pos.z), rot, parent);

        List<Node> nodes = new List<Node>();
        for (int i = 0; i < obj.bounds.x; i++)
        {
            Node node = NodeManager.Instance.GetNodeAtPosition(new Vector3(pos.x + i, 0, pos.z));
            nodes.Add(node);

            if (node != null)
            {
                if (!top)
                {
                    Vector3 pathNodePos = new Vector3(node.pos.x, 0, node.pos.z - 1);
                    if (pathNodePos.x > 0 && pathNodePos.x < WorldGenerator.instance.mapSize && pathNodePos.z > 0 && pathNodePos.z < WorldGenerator.instance.mapSize)
                    {
                        enclosurePathNodes.Add(NodeManager.Instance.GetNodeAtPosition(pathNodePos));
                    }
                }
                else
                {
                    Vector3 pathNodePos = new Vector3(node.pos.x, 0, node.pos.z + 1);
                    if (pathNodePos.x > 0 && pathNodePos.x < WorldGenerator.instance.mapSize && pathNodePos.z > 0 && pathNodePos.z < WorldGenerator.instance.mapSize)
                    {
                        enclosurePathNodes.Add(NodeManager.Instance.GetNodeAtPosition(pathNodePos));
                    }
                }
            }
        }

        NodeManager.Instance.SetExistingNodesUsed(nodes);
    }

    private void SpawnFenceOnZ(SpawnObject obj, Vector3 pos, Quaternion rot, bool left)
    {
        GameObject fence = Instantiate(obj.gameObject, new Vector3(pos.x, pos.y, (pos.z + obj.GetRotatedBounds().z + pos.z) / 2), rot, parent);

        List<Node> nodes = new List<Node>();
        for (int i = 0; i < obj.GetRotatedBounds().z; i++)
        {
            Node node = NodeManager.Instance.GetNodeAtPosition(new Vector3(pos.x, 0, pos.z + i));
            nodes.Add(node);

            if (node != null)
            {
                if (!left)
                {
                    Vector3 pathNodePos = new Vector3(node.pos.x + 1, 0, node.pos.z);
                    if (pathNodePos.x > 0 && pathNodePos.x < WorldGenerator.instance.mapSize && pathNodePos.z > 0 && pathNodePos.z < WorldGenerator.instance.mapSize)
                    {
                        enclosurePathNodes.Add(NodeManager.Instance.GetNodeAtPosition(pathNodePos));
                    }
                }
                else
                {
                    Vector3 pathNodePos = new Vector3(node.pos.x - 1, 0, node.pos.z);
                    if (pathNodePos.x > 0 && pathNodePos.x < WorldGenerator.instance.mapSize && pathNodePos.z > 0 && pathNodePos.z < WorldGenerator.instance.mapSize)
                    {
                        enclosurePathNodes.Add(NodeManager.Instance.GetNodeAtPosition(pathNodePos));
                    }
                }
            }
        }

        NodeManager.Instance.SetExistingNodesUsed(nodes);
    }
}
