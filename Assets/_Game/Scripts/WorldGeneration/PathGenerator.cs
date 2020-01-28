﻿using System.Collections;
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

    public bool AddCentralAreaToPathDest()
    {
        currentNode = NodeManager.Instance.GetNodeAtPosition(new Vector3(WorldGenerator.Instance.mapSize / 2, 0, 4));

        Instantiate(zooEntrances[Random.Range(0, zooEntrances.Length)], new Vector3(currentNode.pos.x, 0, currentNode.pos.z - 2), Quaternion.identity);

        NodeManager.Instance.SetExistingNodesUsed(NodeManager.Instance.GetNodesInRange(new Vector3(currentNode.pos.x - 5, 0, currentNode.pos.z), new Vector3(currentNode.pos.x + 5, 0, currentNode.pos.z + 5)));

        //

        Node centralAreaNode = NodeManager.Instance.GetRandomUnusedNode(WorldGenerator.Instance.mapSize / 5);
        NodeManager.Instance.pathDests.Add(centralAreaNode);

        // SPAWN CENTRAL AREA STUFF HERE
        //BuildingSpawner.Instance.SpawnBuildingAtPos(new Vector3(centralAreaNode.pos.x - 10, 0, centralAreaNode.pos.z), Quaternion.Euler(-90, -90, 0));
        //BuildingSpawner.Instance.SpawnBuildingAtPos(new Vector3(centralAreaNode.pos.x, 0, centralAreaNode.pos.z + 10), Quaternion.Euler(-90, 0, 0));
        //BuildingSpawner.Instance.SpawnBuildingAtPos(new Vector3(centralAreaNode.pos.x + 10, 0, centralAreaNode.pos.z), Quaternion.Euler(-90, 90, 0));
        CentralAreaSpawner.Instance.SpawnCentralAreaAtPos(centralAreaNode.pos);

        return true;
    }

    public void DrawPath()
    {       
        StartCoroutine(RunPathfinding());
    }

    private IEnumerator RunPathfinding()
    {      
        SpawnObject pathPiece = collection[Random.Range(0, collection.Length)];

        foreach (Node destNode in NodeManager.Instance.pathDests)
        {
            yield return new WaitUntil(() => PathSpawner.Instance.DrawPath(destNode, pathPiece, parent));
            yield return null;
        }
        WorldGenerator.Instance.CompletedGeneration();
    }
}
