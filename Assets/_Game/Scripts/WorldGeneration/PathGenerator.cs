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

    [SerializeField]
    private GameObject proceduralPath;

    public Node currentNode;

    private bool completedPathSpawns = false;

    public bool CompletedPathSpawns()
    {
        return completedPathSpawns;
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
        }

        completedPathSpawns = true;
    }
}
