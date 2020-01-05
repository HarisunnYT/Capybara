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
    private int nodeCount;
        
    [SerializeField]
    private List<Vector3> pathNodes = new List<Vector3>();

    void Awake()
    {
        instance = this;
    }

    public void SpawnPath()
    {
        int index = Random.Range(0, collection.Length);

        SpawnObject pathPiece = collection[index];

        for (int i = 0; i < nodeCount; i++)
        {
            pathNodes.Add(new Vector3(Random.Range(-WorldGenerator.instance.mapSize, WorldGenerator.instance.mapSize), 0, Random.Range(-WorldGenerator.instance.mapSize, WorldGenerator.instance.mapSize)));
        }

        for (int i = 0; i < pathNodes.Count; i++)
        {
            Vector3 currentPos = pathNodes[i];

            if (i + 1 < pathNodes.Count)
            {
                //do
                //{
                    GameObject path = Instantiate(collection[index].gameObject, currentPos, Quaternion.identity, parent);
                    path.transform.LookAt(pathNodes[i + 1]);


                float pathAngle = Vector3.Angle(currentPos, pathNodes[i + 1]);
                //if (pathAngle > 90)
                    //pathAngle = 180 - pathAngle;

                Debug.Log("Path Angle: " + pathAngle + " & " + Mathf.Cos(pathAngle * Mathf.Rad2Deg));

                float zChange = (Mathf.Sin(pathAngle) * Mathf.Rad2Deg) / collection[index].bounds;
                float xChange = (Mathf.Cos(pathAngle) * Mathf.Rad2Deg) / collection[index].bounds;
                Debug.Log("z: " + zChange);
                Debug.Log("x: " + xChange);

                Vector3 newPos = new Vector3(currentPos.x + xChange, currentPos.y, currentPos.z + zChange);

                GameObject nextPath = Instantiate(collection[index].gameObject, newPos, Quaternion.identity, parent);
                nextPath.transform.LookAt(pathNodes[i + 1]);

                //

                //Vector3 newPos = new Vector3(path.transform.localPosition.x + collection[index].bounds, path.transform.localPosition.y, path.transform.localPosition.z);
                ////Debug.Log(newPos);

                //    if (Vector3.Distance(currentPos, pathNodes[i + 1]) < Vector3.Distance(newPos, pathNodes[i + 1]))
                //    {
                //        float newX = newPos.x * -1;
                //        newPos = new Vector3(newX, path.transform.localPosition.y, path.transform.localPosition.z);
                //    }

                //    currentPos = newPos;

                    SavePath(path.GetComponent<SpawnObject>());

                //}
                //while (Vector3.Distance(currentPos, pathNodes[i + 1]) > pathPiece.bounds);
            }
        }
    }

    private void SavePath(SpawnObject path)
    {
        SpawnLibrary.instance.spawnedPaths.Add(path);
    }
}
