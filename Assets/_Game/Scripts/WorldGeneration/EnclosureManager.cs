using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnclosureManager : Singleton<EnclosureManager>
{
    public List<SpawnObject> enclosures;

    [SerializeField]
    private int zooBorder = 5;

    [SerializeField]
    private LayerMask conflictLayer;

    public void EnclosureSpawn(int index, int mapSize)
    {
        //Node node = NodeManager.Instance.GetRandomUnusedNodeInRange(new Vector3(Mathf.Clamp(zooBorder + (mapSize / (EnclosureSpawner.Instance.spawnCount - index)), 1, mapSize * .75f), 0,
        //    1 + Mathf.Clamp(zooBorder, 1, mapSize)),
        //        new Vector3(Mathf.Clamp(mapSize / (EnclosureSpawner.Instance.spawnCount - index), 1, mapSize), 0, Mathf.Clamp(mapSize - zooBorder, 1, mapSize)));

        Node node = NodeManager.Instance.GetRandomUnusedNode();

        while (Physics.OverlapSphere(node.pos, EnclosureSpawner.Instance.maxFencesPerSide * 2, conflictLayer).Length > 0)
        {
            Debug.Log("Enclosure collision");
            node = NodeManager.Instance.GetRandomUnusedNode();
        }

        //EnclosureSpawner.Instance.SpawnEnclosure(node);
        EnclosureSpawner.Instance.SpawnPrefabbedEnclosure(node.pos);
    }

    public SpawnObject GetRandomEnclosureBySize(int xLength, int zLength)
    {
        string size = xLength.ToString() + "x" + zLength.ToString();

        SpawnObject enclosure = enclosures.Find(x => x.name.Contains(size));

        return enclosure;
    }
}
