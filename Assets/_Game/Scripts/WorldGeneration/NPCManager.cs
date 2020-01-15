using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : Singleton<NPCManager>
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

    public void InitSpawnNPCs()
    {
        foreach (SpawnGroup spawnGroup in spawnGroups)
        {
            NPCSpawner.Instance.SpawnNPCs(spawnGroup);
        }
    }
}
