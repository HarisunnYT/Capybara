using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerator : Singleton<PathGenerator>
{
    public Transform parent;

    [SerializeField]
    private SpawnObject[] collection;

    [SerializeField]
    private SpawnObject[] zooEntrances;

    public Node currentNode;

    public void AddCentralAreaToPathDest()
    {
        NodeManager.Instance.pathDests.Add(NodeManager.Instance.GetRandomUnusedNode());
    }

    public void DrawPath()
    {
        currentNode = NodeManager.Instance.GetRandomUnusedNode();

        Instantiate(zooEntrances[Random.Range(0, zooEntrances.Length)], new Vector3(currentNode.pos.x - 2, currentNode.pos.y, currentNode.pos.z), Quaternion.identity);       

        StartCoroutine(RunPathfinding());
    }

    private IEnumerator RunPathfinding()
    {      
        SpawnObject pathPiece = collection[Random.Range(0, collection.Length)];

        foreach (Node destNode in NodeManager.Instance.pathDests)
        {
            yield return new WaitUntil(() => PathSpawner.Instance.DrawPath(destNode, pathPiece, parent));

            //List<Node> path = Pathfinding.Instance.FindNodePath(currentNode, destNode);

            //if (path != null)
            //{
            //    foreach (Node node in path)
            //    {
            //        Instantiate(pathPiece.gameObject, node.pos, Quaternion.identity, parent);
            //        NodeManager.Instance.SetNodeAsPath(node.pos);
            //        currentNode = node;
            //    }
            //}
        }
        WorldGenerator.instance.CompletedGeneration();
    }
}
