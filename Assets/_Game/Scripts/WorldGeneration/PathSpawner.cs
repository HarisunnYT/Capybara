using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawner : MonoBehaviour
{
    public static PathSpawner instance;

    [SerializeField]
    private Transform parent;

    [SerializeField]
    private SpawnObject[] collection;

    [SerializeField]
    private int pathCount = 3;

    private List<Vector3> pathDests = new List<Vector3>();

    private Node currentNode;
    private List<Node> confirmedPathNodes = new List<Node>();
    private List<Node> pathNodeCandidates = new List<Node>();

    private Node GetBestNodeForPath(List<Node> nodesToCheck, Vector3 target)
    {
        Node bestNode = null;

        foreach(Node node in nodesToCheck)
        {
            float distToTarget = Vector3.Distance(node.pos, target);

            if (bestNode == null || distToTarget < bestNode.distanceToDest)
            {
                node.distanceToDest = distToTarget;
                bestNode = node;
            }
        }

        return bestNode;
    }

    void Awake()
    {
        instance = this;
    }

    public void AssignPathNodes()
    {
        currentNode = NodeManager.instance.GetRandomUnusedNode();

        confirmedPathNodes.Add(currentNode);

        StartCoroutine(DrawPath());   
    }

    private IEnumerator DrawPath()
    {
        SpawnObject pathPiece = collection[Random.Range(0, collection.Length)];
        confirmedPathNodes.Clear();

        for (int i = 0; i < pathCount; i++)
        {
            pathDests.Add(NodeManager.instance.GetRandomUnusedNode().pos);
        }
        
        foreach (Vector3 dest in pathDests)
        {
            do
            {
                pathNodeCandidates.Clear();

                pathNodeCandidates.Add(NodeManager.instance.GetNodeAtPosition(new Vector3(currentNode.pos.x, 0, currentNode.pos.z + 1)));
                pathNodeCandidates.Add(NodeManager.instance.GetNodeAtPosition(new Vector3(currentNode.pos.x + 1, 0, currentNode.pos.z)));
                pathNodeCandidates.Add(NodeManager.instance.GetNodeAtPosition(new Vector3(currentNode.pos.x, 0, currentNode.pos.z - 1)));
                pathNodeCandidates.Add(NodeManager.instance.GetNodeAtPosition(new Vector3(currentNode.pos.x - 1, 0, currentNode.pos.z)));                

                if (pathNodeCandidates[0].used && pathNodeCandidates[1].used && pathNodeCandidates[2].used && pathNodeCandidates[3].used)
                {
                    break;
                }

                List<Node> pathNodeCadidatesCopy = pathNodeCandidates;
                List<Node> goodNodes = new List<Node>(); 
                foreach (Node node in pathNodeCadidatesCopy)
                {
                    if (node != null && !node.used || node != null && confirmedPathNodes.Count > 0 && !confirmedPathNodes.Contains(node))
                    {
                        //pathNodeCandidates.Remove(node);
                        goodNodes.Add(node);
                    }
                }

                Node nextNode = GetBestNodeForPath(goodNodes, dest);
                confirmedPathNodes.Add(nextNode);
                currentNode = nextNode;

                PlacePath(confirmedPathNodes, pathPiece);
                confirmedPathNodes.Clear();
            }
            while (Vector3.Distance(currentNode.pos, dest) != 0);

            yield return null;
        }
        WorldGenerator.instance.CompletedGeneration();
    }

    private void PlacePath(List<Node> nodesToPlace, SpawnObject pathPiece)
    {
        foreach (Node node in nodesToPlace)
        {
            // draw path on node
            Instantiate(pathPiece.gameObject, node.pos, Quaternion.identity, parent);
            NodeManager.instance.SetNodeUsed(node.pos);
            SpawnLibrary.instance.spawnedPaths.Add(pathPiece);
        }     
    }
}
