using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : Singleton<ObjectSpawner>
{
    public void SpawnObjects(ObjectManager.SpawnGroup spawnGroup)
    {
        for (int i = 0; i < spawnGroup.spawnCount; i++)
        {
            PlaceItem(spawnGroup, WorldQuadrants.Instance.GetQuadrant(i, spawnGroup.spawnCount));
        }
    }

    private void PlaceItem(ObjectManager.SpawnGroup spawnGroup, int quadrantIndex)
    {
        int index = Random.Range(0, spawnGroup.collection.Length);
        Quaternion rot = Quaternion.Euler(0, Random.Range(0, 360), 0);

        Vector3 pos = WorldQuadrants.Instance.GetSpawnPosInQuadrant(quadrantIndex, spawnGroup.collection[index].MaxBounds(), spawnGroup.conflictLayer);

        Instantiate(spawnGroup.collection[index].gameObject, new Vector3(pos.x, spawnGroup.collection[index].bounds.y, pos.z), rot, spawnGroup.parent);
        NodeManager.Instance.SetNodeUsed(pos);
    }

    public void PlaceObjectFromSpawner(ObjectManager.SpawnGroup spawnGroup, Vector3 pos)
    {
        int index = Random.Range(0, spawnGroup.collection.Length);

        SpawnObject obj = Instantiate(spawnGroup.collection[Random.Range(0, spawnGroup.collection.Length)], pos, Quaternion.identity, spawnGroup.parent);

        //List<Node> nodes = NodeManager.Instance.GetNodesInRange(new Vector3(pos.x - obj.bounds.x / 2, 0, pos.z - obj.bounds.z / 2), new Vector3(pos.x + obj.bounds.x / 2, 0, pos.z + obj.bounds.z / 2));
        //foreach(Node node in nodes)
        //{
        //    NodeManager.Instance.SetNodeUsed(node.pos);
        //}       
    }
}
