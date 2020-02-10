using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSpawner : Singleton<PathSpawner>
{
    [SerializeField]
    private bool spawnPathTiles;

    [SerializeField]
    private int pathPaddingPerSide = 2;

    [SerializeField]
    private List<SpawnObject> pathFoilageList = new List<SpawnObject>();
    private SpawnObject pathFoilage;

    private SpawnObject pathPiece;

    private List<Node> confirmedPathNodes = new List<Node>();
    private List<Node> pathNodeCandidates = new List<Node>();

    private const int distUntilPrecisePlacement = 12;

    Node prevNode = null;

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
        nodeCandidate = NodeManager.Instance.GetNodeAtPosition(new Vector3(PathGenerator.Instance.currentNode.pos.x, 0, PathGenerator.Instance.currentNode.pos.z + pathPiece.bounds.z));
        if (nodeCandidate != null)
        {
            nodeCandidates.Add(nodeCandidate);
        }

        nodeCandidate = NodeManager.Instance.GetNodeAtPosition(new Vector3(PathGenerator.Instance.currentNode.pos.x + pathPiece.bounds.x, 0, PathGenerator.Instance.currentNode.pos.z));
        if (nodeCandidate != null)
        {
            nodeCandidates.Add(nodeCandidate);
        }

        nodeCandidate = NodeManager.Instance.GetNodeAtPosition(new Vector3(PathGenerator.Instance.currentNode.pos.x, 0, PathGenerator.Instance.currentNode.pos.z - pathPiece.bounds.z));
        if (nodeCandidate != null)
        {
            nodeCandidates.Add(nodeCandidate);
        }

        nodeCandidate = NodeManager.Instance.GetNodeAtPosition(new Vector3(PathGenerator.Instance.currentNode.pos.x - pathPiece.bounds.x, 0, PathGenerator.Instance.currentNode.pos.z));
        if (nodeCandidate != null)
        {
            nodeCandidates.Add(nodeCandidate);
        }

        return nodeCandidates;
    }

    public bool DrawPath(Node destinationNode, SpawnObject selectedPathPiece, Transform parent)
    {
        pathFoilage = pathFoilageList[Random.Range(0, pathFoilageList.Count)];
        pathPiece = selectedPathPiece;

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

                    //for (int i = 1; i <= pathPaddingPerSide; i++)
                    //{
                    //    if(PathGenerator.Instance.currentNode.pos.x > nextNode.pos.x || PathGenerator.Instance.currentNode.pos.x < nextNode.pos.x)
                    //    {
                    //        if (!NodeManager.Instance.GetNodeAtPosition(new Vector3(nextNode.pos.x, nextNode.pos.y, nextNode.pos.z + pathPaddingPerSide)).used)
                    //        {
                    //            confirmedPathNodes.Add(NodeManager.Instance.GetNodeAtPosition(new Vector3(nextNode.pos.x, nextNode.pos.y, nextNode.pos.z + pathPaddingPerSide)));
                    //        }
                    //        if (!NodeManager.Instance.GetNodeAtPosition(new Vector3(nextNode.pos.x, nextNode.pos.y, nextNode.pos.z - pathPaddingPerSide)).used)
                    //        {
                    //            confirmedPathNodes.Add(NodeManager.Instance.GetNodeAtPosition(new Vector3(nextNode.pos.x, nextNode.pos.y, nextNode.pos.z - pathPaddingPerSide)));
                    //        }
                    //    }
                    //    else
                    //    {
                    //        if (!NodeManager.Instance.GetNodeAtPosition(new Vector3(nextNode.pos.x + pathPaddingPerSide, nextNode.pos.y, nextNode.pos.z)).used)
                    //        {
                    //            confirmedPathNodes.Add(NodeManager.Instance.GetNodeAtPosition(new Vector3(nextNode.pos.x + pathPaddingPerSide, nextNode.pos.y, nextNode.pos.z)));
                    //        }
                    //        if (!NodeManager.Instance.GetNodeAtPosition(new Vector3(nextNode.pos.x - pathPaddingPerSide, nextNode.pos.y, nextNode.pos.z)).used)
                    //        {
                    //            confirmedPathNodes.Add(NodeManager.Instance.GetNodeAtPosition(new Vector3(nextNode.pos.x - pathPaddingPerSide, nextNode.pos.y, nextNode.pos.z)));
                    //        }
                    //    }
                    //}
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
        //foreach (Node node in nodesToPlace)
        for (int i = 0; i < nodesToPlace.Count; i++)
        {
            // draw path on node
            if(nodesToPlace[i] != null)
            {
                if (spawnPathTiles)
                {
                    Instantiate(pathPiece.gameObject, nodesToPlace[i].pos, Quaternion.identity, parent);
                    //if (i == 0 && nodesToPlace.Count > 2)
                    //    PathLining(nodesToPlace[0], nodesToPlace[1], parent, nodesToPlace.Count);
                }
                
                NodeManager.Instance.SetNodeAsPath(nodesToPlace[i].pos);

                // update ferr path
                //ProceduralPath.Instance.AddControlPoint(node.pos.x, node.pos.z, 0.5f);
            }            
        }     
    }

    //private void PathLining(Node node, Node neighbourNode, Transform parent, int width)
    //{
    //    if (node.pos.x < neighbourNode.pos.x || node.pos.x > neighbourNode.pos.x)
    //    {
    //        if (!NodeManager.Instance.GetNodeAtPosition(new Vector3(node.pos.x, node.pos.y, node.pos.z + pathPaddingPerSide * width)).used)
    //        {
    //            Vector3 pos = new Vector3(node.pos.x, node.pos.y, node.pos.z + pathPaddingPerSide * width);
    //            Instantiate(pathFoilage, pos, Quaternion.identity, parent);
    //            NodeManager.Instance.SetNodeUsed(pos);
    //        }
    //        if (!NodeManager.Instance.GetNodeAtPosition(new Vector3(node.pos.x, node.pos.y, node.pos.z - pathPaddingPerSide * width)).used)
    //        {
    //            Vector3 pos = new Vector3(node.pos.x, node.pos.y, node.pos.z - pathPaddingPerSide * width);
    //            Instantiate(pathFoilage, pos, Quaternion.identity, parent);
    //            NodeManager.Instance.SetNodeUsed(pos);
    //        }
    //    }
    //    else
    //    {
    //        if (!NodeManager.Instance.GetNodeAtPosition(new Vector3(node.pos.x + pathPaddingPerSide * width, node.pos.y, node.pos.z)).used)
    //        {
    //            Vector3 pos = new Vector3(node.pos.x + pathPaddingPerSide * width, node.pos.y, node.pos.z);
    //            Instantiate(pathFoilage, pos, Quaternion.identity, parent);
    //            NodeManager.Instance.SetNodeUsed(pos);
    //        }
    //        if (!NodeManager.Instance.GetNodeAtPosition(new Vector3(node.pos.x - pathPaddingPerSide * width, node.pos.y, node.pos.z)).used)
    //        {
    //            Vector3 pos = new Vector3(node.pos.x - pathPaddingPerSide * width, node.pos.y, node.pos.z);
    //            Instantiate(pathFoilage, pos, Quaternion.identity, parent);
    //            NodeManager.Instance.SetNodeUsed(pos);
    //        }
    //    }
    //}
}
