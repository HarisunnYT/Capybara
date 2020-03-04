using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnclosureManager : Singleton<EnclosureManager>
{
    public List<SpawnObject> enclosures;

    public int spawnCount;

    public bool InitEnclosureSpawn()
    {
        RoomSpawner.Instance.SpawnPrefabbedEnclosures();

        return true;
    }

    public SpawnObject GetRandomEnclosureBySize(int xLength, int zLength)
    {
        string size = xLength.ToString() + "x" + zLength.ToString();

        SpawnObject enclosure = enclosures.Find(x => x.name.Contains(size));

        return enclosure;
    }
}
