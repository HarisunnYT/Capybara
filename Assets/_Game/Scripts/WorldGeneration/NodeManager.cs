using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeManager : MonoBehaviour
{
    public static NodeManager instance;

    int mapSize;
    public List<Node> nodes = new List<Node>();

    private void Start()
    {
        instance = this;

        mapSize = WorldGenerator.instance.mapSize;
        DrawNodes();
    }

    private void DrawNodes()
    {
        Vector3 origin = Vector3.zero;

        for (int x = 0; x < mapSize; x++)
        {
            for (int z = 0; z < mapSize; z++)
            {
                Vector3 nodePos = new Vector3(x + 1, 0, z + 1) + origin;

                Node node = new Node();
                node.pos = nodePos;
                node.used = false;

                nodes.Add(node);
            }
        }
    }

    private void OnDrawGizmos()
    {
        foreach (Node node in nodes)
        {
            if (!node.used)
            {
                Gizmos.color = Color.blue;
                Gizmos.DrawCube(node.pos, new Vector3(1, 0.1f, 1));
            }
            else
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(node.pos, new Vector3(1, 0.1f, 1));
            }
        }
    }

    public Node GetNodeAtPosition(Vector3 pos)
    {
        Node node = nodes.Find(x => x.pos == pos);
        return node;
    }

    public List<Node> GetNodesInRange(Vector3 posOne, Vector3 posTwo)
    {
        int lowerX = Mathf.RoundToInt(Mathf.Min(posOne.x, posTwo.x));
        int lowerZ = Mathf.RoundToInt(Mathf.Min(posOne.z, posTwo.z));
        int upperX = Mathf.RoundToInt(Mathf.Max(posOne.x, posTwo.x));
        int upperZ = Mathf.RoundToInt(Mathf.Max(posOne.z, posTwo.z));

        int lowest = Mathf.RoundToInt(Mathf.Min(lowerX, lowerZ));
        int highest = Mathf.RoundToInt(Mathf.Max(upperX, upperZ));

        //int xMod = maxX < xPos ? xPos *= -1 : xPos *= 1;
        //int zMod = maxZ < zPos ? zPos *= -1 : zPos *= 1;

        List<Node> nodes = new List<Node>();

        for (int x = lowest; x < highest; x++)
        {
            for (int z = lowest; z < highest; z++)
            {
                if (x >= lowerX && x <= upperX && z >= lowerZ && z <= upperZ)
                {
                    nodes.Add(GetNodeAtPosition(new Vector3(x, 0, z)));
                }
            }
        }

        return nodes;
    }

    public void SetNodeUsed(Vector3 pos)
    {
        GetNodeAtPosition(pos).used = true;
    }

    public Node GetRandomUnusedNode()
    {
        Node node = null;

        do
        {
            node = NodeManager.instance.nodes[Random.Range(0, NodeManager.instance.nodes.Count)];
        }
        while (node != null && node.used);

        return node;
    }
}
