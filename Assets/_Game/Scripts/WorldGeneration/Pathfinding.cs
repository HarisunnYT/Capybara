using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : Singleton<Pathfinding>
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    private List<Node> openList;
    private List<Node> closedList;

    public List<Node> FindNodePath(Node currentNode, Node destNode)
    {
        Node startNode = currentNode;
        Node endNode = destNode;

        openList = new List<Node> { startNode };
        closedList = new List<Node>();

        for (int x = 1; x < WorldGenerator.instance.mapSize; x++)
        {
            for (int z = 1; z < WorldGenerator.instance.mapSize; z++)
            {
                Node pathNode = NodeManager.instance.GetNodeAtPositionWithString(new Vector3(x, 0, z).ToString());
                pathNode.gCost = int.MaxValue;
                pathNode.CalculateFCost();
                pathNode.cameFromNode = null;
            }
        }
        startNode.gCost = 0;
        startNode.hCost = CalculateDistanceCost(startNode, endNode);
        startNode.CalculateFCost();

        while (openList.Count > 0)
        {
            Node node = GetLowestFCostNode(openList);
            if (node == endNode)
            {
                // reached final node
                return CalculatePath(endNode);
            }
            openList.Remove(node);
            closedList.Add(node);

            foreach (Node neighbourNode in GetNeighbourList(node))
            {
                if (closedList.Contains(neighbourNode))
                {
                    continue;
                }

                if (neighbourNode != null)
                {
                    int tentativeGCost = node.gCost + CalculateDistanceCost(node, neighbourNode);
                    if (tentativeGCost < neighbourNode.gCost)
                    {
                        neighbourNode.cameFromNode = node;
                        neighbourNode.gCost = tentativeGCost;
                        neighbourNode.hCost = CalculateDistanceCost(neighbourNode, endNode);
                        neighbourNode.CalculateFCost();

                        if (!openList.Contains(neighbourNode))
                        {
                            openList.Add(neighbourNode);                         
                        }
                    }
                }            
            }
        }
        // out of nodes on the openList
        return null;
    }

    private List<Node> CalculatePath(Node endNode)
    {
        List<Node> path = new List<Node>();
        path.Add(endNode);
        Node node = endNode;
        while (node.cameFromNode != null)
        {
            path.Add(node.cameFromNode);
            node = node.cameFromNode;
        }
        path.Reverse();
        return path;
    }

    private int CalculateDistanceCost(Node a, Node b)
    {
        int xDistance = 0;
        int zDistance = 0;
        int remaining = 0;

        if (a != null && b != null)
        {
            xDistance = Mathf.Abs(Mathf.RoundToInt(a.pos.x - b.pos.z));
            zDistance = Mathf.Abs(Mathf.RoundToInt(a.pos.z - b.pos.x));
            remaining = Mathf.Abs(xDistance - zDistance);
        }      
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, zDistance) + MOVE_STRAIGHT_COST * remaining;
    }

    private Node GetLowestFCostNode(List<Node> pathNodeList)
    {
        Node lowestFCostNode = pathNodeList[0];
        for (int i = 1; i < pathNodeList.Count; i++)
        {
            if (pathNodeList[i].fCost < lowestFCostNode.fCost)
            {
                lowestFCostNode = pathNodeList[i];
            }
        }
        return lowestFCostNode;
    }

    private List<Node> GetNeighbourList(Node node)
    {
        List<Node> neighbourList = new List<Node>();

        if (node.pos.x - 1 >= 1)
        {
            // left
            Node nodeCandidate = NodeManager.instance.GetNodeAtPositionWithString(new Vector3(node.pos.x - 1, 0, node.pos.z).ToString());
            if (nodeCandidate != null && !nodeCandidate.used)
                neighbourList.Add(nodeCandidate);
            // left down
            if (node.pos.z - 1 >= 1)
            {
                nodeCandidate = NodeManager.instance.GetNodeAtPositionWithString(new Vector3(node.pos.x - 1, 0, node.pos.z - 1).ToString());
                if (nodeCandidate != null && !nodeCandidate.used)
                    neighbourList.Add(nodeCandidate);
            }
            // left up
            if (node.pos.z + 1 < WorldGenerator.instance.mapSize)
            {
                nodeCandidate = NodeManager.instance.GetNodeAtPositionWithString(new Vector3(node.pos.x - 1, 0, node.pos.z + 1).ToString());
                if (nodeCandidate != null && !nodeCandidate.used)
                    neighbourList.Add(nodeCandidate);
            }
        }
        if (node.pos.x + 1 < WorldGenerator.instance.mapSize)
        {
            // right
            Node nodeCandidate = NodeManager.instance.GetNodeAtPositionWithString(new Vector3(node.pos.x + 1, 0, node.pos.z).ToString());
            if (nodeCandidate != null && !nodeCandidate.used)
                neighbourList.Add(nodeCandidate);
            // right down
            if (node.pos.z - 1 >= 1)
            {
                nodeCandidate = NodeManager.instance.GetNodeAtPositionWithString(new Vector3(node.pos.x + 1, 0, node.pos.z - 1).ToString());
                if (nodeCandidate != null && !nodeCandidate.used)
                    neighbourList.Add(nodeCandidate);
            }
            // right up
            if (node.pos.z + 1 < WorldGenerator.instance.mapSize)
            {
                nodeCandidate = NodeManager.instance.GetNodeAtPositionWithString(new Vector3(node.pos.x + 1, 0, node.pos.z + 1).ToString());
                if (nodeCandidate != null && !nodeCandidate.used)
                    neighbourList.Add(nodeCandidate);
            }
        }
        // down
        if (node.pos.z - 1 >= 1)
        {
            Node nodeCandidate = NodeManager.instance.GetNodeAtPositionWithString(new Vector3(node.pos.x, 0, node.pos.y - 1).ToString());
            if (nodeCandidate != null && !nodeCandidate.used)
                neighbourList.Add(nodeCandidate);
        }
        // up
        if (node.pos.y + 1 < WorldGenerator.instance.mapSize)
        {
            Node nodeCandidate = NodeManager.instance.GetNodeAtPositionWithString(new Vector3(node.pos.x, 0, node.pos.y + 1).ToString());
            if (nodeCandidate != null && !nodeCandidate.used)
                neighbourList.Add(nodeCandidate);
        }

        return neighbourList;
    }
}
