using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSpawner : Singleton<RoomSpawner>
{
    private const int enclosureGroupSpawnChance = 80;

    [SerializeField]
    private List<Spawner> roomSpawners = new List<Spawner>();

    public void SpawnPrefabbedEnclosures()
    {
        for (int x = WorldGenerator.Instance.segmentSize; x < WorldGenerator.Instance.mapSize; x += WorldGenerator.Instance.segmentSize)
        {
            for (int z = WorldGenerator.Instance.segmentSize; z < WorldGenerator.Instance.mapSize; z += WorldGenerator.Instance.segmentSize)
            {
                Spawner spawnedSpawner = Instantiate(WorldGenerator.Instance.spawner, new Vector3(x, 0, z), Quaternion.identity);
                spawnedSpawner.spawnType = Spawner.SpawnType.EnclosureGroup;
                spawnedSpawner.spawnChance = enclosureGroupSpawnChance;

                roomSpawners.Add(spawnedSpawner);
            }
        }
        SpawnRooms();
    }
    
    private void SpawnRooms()
    {
        if (!WorldGenerator.Instance.bossRoomSpawned)
        {
            int index = Random.Range(0, roomSpawners.Count);
            roomSpawners[index].SpawnRoom(GetRoomOfType(Room.RoomType.boss), true);
            roomSpawners.Remove(roomSpawners[index]);
        }

        if (!WorldGenerator.Instance.treasureRoomSpawned)
        {
            int index = Random.Range(0, roomSpawners.Count);
            roomSpawners[index].SpawnRoom(GetRoomOfType(Room.RoomType.treasure), true);
            roomSpawners.Remove(roomSpawners[index]);
        }

        foreach (Spawner roomSpawner in roomSpawners)
        {
            roomSpawner.SpawnRoom(GetRoomOfType(Room.RoomType.standard));
        }       
    }

    private SpawnObject GetRoomOfType(Room.RoomType roomType)
    {
        ObjectManager.SpawnGroup spawnGroup = ObjectManager.Instance.GetSpawnGroup("EnclosureGroup");

        SpawnObject spawnObj = spawnGroup.collection[Random.Range(0, spawnGroup.collection.Length)];

        while (spawnObj.GetComponent<Room>().roomType != roomType)
        {
            spawnObj = spawnGroup.collection[Random.Range(0, spawnGroup.collection.Length)];
        }    

        return spawnObj;
    }
}
