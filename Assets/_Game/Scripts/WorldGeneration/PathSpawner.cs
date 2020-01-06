using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawner : MonoBehaviour
{
    public static PathSpawner instance;

    public Transform parent;

    [SerializeField]
    private SpawnObject[] collection;

    [SerializeField]
    private SpawnObject[] zooEntrances;

    [SerializeField]
    private int pathCount = 3;

    //private List<Vector3> pathDests = new List<Vector3>();

    private Node currentNode;
    private List<Node> confirmedPathNodes = new List<Node>();
    private List<Node> pathNodeCandidates = new List<Node>();

    private Node GetBestNodeForPath(List<Node> nodesToCheck, Vector3 target)
    {
        Node bestNode = null;

        foreach(Node node in nodesToCheck)
        {
            if (node != null)
            {
                float distToTarget = Vector3.Distance(node.pos, target);

                if (bestNode == null || distToTarget < bestNode.distanceToDest)
                {
                    node.distanceToDest = distToTarget;
                    bestNode = node;
                }
            }
        }

        return bestNode;
    }

    private List<Node> GetPathNodeCandidates()
    {
        List<Node> nodeCandidates = new List<Node>();

        Node nodeCandidate;
        nodeCandidate = NodeManager.instance.GetNodeAtPosition(new Vector3(currentNode.pos.x, 0, currentNode.pos.z + 1));
        if (nodeCandidate != null)
        {
            nodeCandidates.Add(nodeCandidate);
        }

        nodeCandidate = NodeManager.instance.GetNodeAtPosition(new Vector3(currentNode.pos.x + 1, 0, currentNode.pos.z));
        if (nodeCandidate != null)
        {
            nodeCandidates.Add(nodeCandidate);
        }

        nodeCandidate = NodeManager.instance.GetNodeAtPosition(new Vector3(currentNode.pos.x, 0, currentNode.pos.z - 1));
        if (nodeCandidate != null)
        {
            nodeCandidates.Add(nodeCandidate);
        }

        nodeCandidate = NodeManager.instance.GetNodeAtPosition(new Vector3(currentNode.pos.x - 1, 0, currentNode.pos.z));
        if (nodeCandidate != null)
        {
            nodeCandidates.Add(nodeCandidate);
        }

        return nodeCandidates;
    }

    void Awake()
    {
        instance = this;        
    }

    public void AddCentralAreaToPathDest()
    {
        NodeManager.instance.pathDests.Add(NodeManager.instance.GetNodeAtPosition(new Vector3(WorldGenerator.instance.mapSize / 2, 0, WorldGenerator.instance.mapSize / 2)));
    }

    public void AssignPathNodes()
    {
        currentNode = NodeManager.instance.GetRandomUnusedNode();    
        StartCoroutine(DrawPath());   
    }

    private IEnumerator DrawPath()
    {
        yield return null;
        Instantiate(zooEntrances[Random.Range(0, zooEntrances.Length)], new Vector3(currentNode.pos.x - 2, currentNode.pos.y, currentNode.pos.z), Quaternion.identity);

        SpawnObject pathPiece = collection[Random.Range(0, collection.Length)];
        confirmedPathNodes.Clear();
        
        foreach (Node destNode in NodeManager.instance.pathDests)
        {
            do
            {
                pathNodeCandidates.Clear();
                pathNodeCandidates = GetPathNodeCandidates();

                yield return new WaitUntil(() => pathNodeCandidates.Count >= 0);

                if ((pathNodeCandidates[0].used || pathNodeCandidates[0] == null) && (pathNodeCandidates[1].used || pathNodeCandidates[1] == null) && 
                    (pathNodeCandidates[2].used || pathNodeCandidates[2] == null) && (pathNodeCandidates[3].used || pathNodeCandidates[3] == null))
                {
                    break;
                }

                List<Node> pathNodeCadidatesCopy = pathNodeCandidates;
                List<Node> goodNodes = new List<Node>(); 
                foreach (Node node in pathNodeCadidatesCopy)
                {
                    if (node != null && !node.used || node != null && confirmedPathNodes.Count > 0 && !confirmedPathNodes.Contains(node))
                    {
                        goodNodes.Add(node);
                    }
                }

                Node nextNode;
                if (goodNodes != null || destNode != null)
                {
                    nextNode = GetBestNodeForPath(goodNodes, destNode.pos);
                }
                else
                {
                    break;
                }

                confirmedPathNodes.Add(nextNode);
                currentNode = nextNode;                   
              
                PlacePath(confirmedPathNodes, pathPiece);
                confirmedPathNodes.Clear();
            }
            while (Vector3.Distance(currentNode.pos, destNode.pos) > 10);
        }
        WorldGenerator.instance.CompletedGeneration();
    }

    private void PlacePath(List<Node> nodesToPlace, SpawnObject pathPiece)
    {
        foreach (Node node in nodesToPlace)
        {
            // draw path on node
            Instantiate(pathPiece.gameObject, node.pos, Quaternion.identity, parent);
            NodeManager.instance.SetNodeAsPath(node.pos);
            SpawnLibrary.instance.spawnedPaths.Add(pathPiece);
        }     
    }
}
