using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : Singleton<ObjectManager>
{
    [System.Serializable]
    public struct SpawnGroup
    {
        public string groupTitle;
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
            ObjectSpawner.Instance.SpawnObjects(spawnGroup);
        }
        return true;
    }
}
