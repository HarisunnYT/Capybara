using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
    [System.Serializable]
    public struct SpawnGroup
    {
        public string groupTitle;
        public bool autoSpawn;
        public Transform parent;
        public int spawnCount;
        public SpawnObject[] collection;
        public LayerMask conflictLayer;
    }

    public SpawnGroup[] spawnGroups;

    public bool InitSpawnObjects()
    {
        foreach (SpawnGroup spawnGroup in spawnGroups)
        {
            if (spawnGroup.autoSpawn)
            {
                ObjectSpawner.Instance.SpawnObjects(spawnGroup);
            }           
        }
        return true;
    }

    public SpawnGroup GetSpawnGroup(string spawnGroupStr)
    {
        foreach (SpawnGroup spawnGroup in spawnGroups)
        {
            if (spawnGroup.groupTitle == spawnGroupStr)
            {
                return spawnGroup;
            }
        }

        return spawnGroups[0];
    }
}
