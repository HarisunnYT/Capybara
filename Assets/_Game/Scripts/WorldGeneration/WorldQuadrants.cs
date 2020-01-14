using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldQuadrants : Singleton<WorldQuadrants>
{
    private const int sectionCount = 4;

    protected override void Initialize()
    {
        
    }

    private List<Vector3> xQuads = new List<Vector3>();
    private List<Vector3> zQuads = new List<Vector3>();

    public void SetupQuadrants()
    {
        xQuads.Add(new Vector3(1, 0, 1));
        xQuads.Add(new Vector3(WorldGenerator.Instance.mapSize / 2, 0, 1));
        xQuads.Add(new Vector3(1, 0, WorldGenerator.Instance.mapSize));
        xQuads.Add(new Vector3(WorldGenerator.Instance.mapSize / 2, 0, WorldGenerator.Instance.mapSize / 2));

        zQuads.Add(new Vector3(WorldGenerator.Instance.mapSize / 2, 0, WorldGenerator.Instance.mapSize / 2));
        zQuads.Add(new Vector3(WorldGenerator.Instance.mapSize, 0, WorldGenerator.Instance.mapSize / 2));
        zQuads.Add(new Vector3(WorldGenerator.Instance.mapSize / 2, 0, WorldGenerator.Instance.mapSize));
        zQuads.Add(new Vector3(WorldGenerator.Instance.mapSize, 0, WorldGenerator.Instance.mapSize));
    }

    public Vector3 GetSpawnPosInQuadrant(int quadrantIndex, float bounds, LayerMask conflictLayer)
    {
        Node node = NodeManager.Instance.GetRandomUnusedNodeInRange(xQuads[quadrantIndex], zQuads[quadrantIndex]);

        while (Physics.OverlapBox(node.pos, new Vector3(bounds, 0, bounds), Quaternion.identity, conflictLayer).Length > 0)
        {
            node = NodeManager.Instance.GetRandomUnusedNodeInRange(xQuads[quadrantIndex], zQuads[quadrantIndex]);
        }

        return node.pos;
    }

    public int GetQuadrant(int index, int spawnCount)
    {
        int groupSize = spawnCount / sectionCount;
        int groupCount = 0;
        int quadrant = 0;

        for (int i = 0; i < spawnCount; i += groupSize)
        {
            if (index < i)
            {
                quadrant = groupCount;
                break;
            }
            groupCount++;
        }

        return quadrant;
    }
}
