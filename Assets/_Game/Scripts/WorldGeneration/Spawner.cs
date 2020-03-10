using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    public enum SpawnType { Pickup, Foliage, EnclosureGroup, Enclosure, Fence, Props };
    public SpawnType spawnType;

    [Range(1, 100)]
    public int spawnChance = 60;

    void Start()
    {
        if (Random.Range(1, 100) <= spawnChance)
        {
            if (spawnType != SpawnType.EnclosureGroup)
            {
                ObjectSpawner.Instance.PlaceObjectFromSpawner(ObjectManager.Instance.GetSpawnGroup(spawnType.ToString()), transform.position, transform.rotation);
            }
        }        
    }

    public void SpawnRoom(SpawnObject spawnObject, bool forceSpawn = false)
    {
        if (Random.Range(1, 100) <= spawnChance || forceSpawn)
        {
            ObjectSpawner.Instance.PlaceRoomFromSpawner(ObjectManager.Instance.GetSpawnGroup(spawnType.ToString()), spawnObject, transform.position);
        }
    }
}
