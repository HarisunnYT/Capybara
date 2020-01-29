using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawner : Singleton<PathSpawner>
{
    [SerializeField]
    private bool spawnPathTiles;

    private List<Node> confirmedPathNodes = new List<Node>();
    private List<Node> pathNodeCandidates = new List<Node>();

    private const int distUntilPrecisePlacement = 12;

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
        nodeCandidate = NodeManager.Instance.GetNodeAtPosition(new Vector3(PathGenerator.Instance.currentNode.pos.x, 0, PathGenerator.Instance.currentNode.pos.z + 1));
        if (nodeCandidate != null)
        {
            nodeCandidates.Add(nodeCandidate);
        }

        nodeCandidate = NodeManager.Instance.GetNodeAtPosition(new Vector3(PathGenerator.Instance.currentNode.pos.x + 1, 0, PathGenerator.Instance.currentNode.pos.z));
        if (nodeCandidate != null)
        {
            nodeCandidates.Add(nodeCandidate);
        }

        nodeCandidate = NodeManager.Instance.GetNodeAtPosition(new Vector3(PathGenerator.Instance.currentNode.pos.x, 0, PathGenerator.Instance.currentNode.pos.z - 1));
        if (nodeCandidate != null)
        {
            nodeCandidates.Add(nodeCandidate);
        }

        nodeCandidate = NodeManager.Instance.GetNodeAtPosition(new Vector3(PathGenerator.Instance.currentNode.pos.x - 1, 0, PathGenerator.Instance.currentNode.pos.z));
        if (nodeCandidate != null)
        {
            nodeCandidates.Add(nodeCandidate);
        }

        return nodeCandidates;
    }

    public bool DrawPath(Node destinationNode, SpawnObject pathPiece, Transform parent)
    {
        if(PathGenerator.Instance.currentNode != null && destinationNode != null)
        {
            while (Vector3.Distance(PathGenerator.Instance.currentNode.pos, destinationNode.pos) > distUntilPrecisePlacement)
            {
                confirmedPathNodes.Clear();
                pathNodeCandidates.Clear();

                pathNodeCandidates = GetPathNodeCandidates();

                if(pathNodeCandidates == null)
                {
                    break;
                }

                List<Node> pathNodeCadidatesCopy = pathNodeCandidates;
                List<Node> goodNodes = new List<Node>();
                foreach (Node node in pathNodeCadidatesCopy)
                {
                    if (node != null && !node.used && !node.enclosure)
                    {
                        goodNodes.Add(node);
                    }
                }

                Node nextNode = null;
                if (goodNodes != null)
                {
                    nextNode = GetBestNodeForPath(goodNodes, destinationNode.pos);
                }

                if (nextNode != null && Vector3.Distance(nextNode.pos, destinationNode.pos) < Vector3.Distance(PathGenerator.Instance.currentNode.pos, destinationNode.pos))
                {
                    confirmedPathNodes.Add(nextNode);
                    PathGenerator.Instance.currentNode = nextNode;

                    PlacePath(confirmedPathNodes, pathPiece, parent);                 
                    confirmedPathNodes.Clear();                 
                }
                else
                {
                    break;
                }
            }
        }       
        return true;
    }

    private void PlacePath(List<Node> nodesToPlace, SpawnObject pathPiece, Transform parent)
    {
        foreach (Node node in nodesToPlace)
        {
            // draw path on node
            if(node != null)
            {
                if (spawnPathTiles)
                {
                    Instantiate(pathPiece.gameObject, node.pos, Quaternion.identity, parent);
                }
                
                NodeManager.Instance.SetNodeAsPath(node.pos);

                // update ferr path
                ProceduralPath.Instance.AddControlPoint(node.pos.x, node.pos.z, 0.5f);
            }            
        }     
    }
}
