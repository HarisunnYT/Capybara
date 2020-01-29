using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ferr;

public class ProceduralPath : Singleton<ProceduralPath>
{
    [HideInInspector]
    public Ferr2DT_PathTerrain pathTerrain;

    public void AddControlPoint(float x, float y, float halfWidth)
    {
        pathTerrain.AddAutoPoint(new Vector2(x - halfWidth, y - halfWidth));
        pathTerrain.AddAutoPoint(new Vector2(x + halfWidth, y + halfWidth));
        pathTerrain.Build();
    }

    public void CompleteProceduralPath()
    {
        pathTerrain.transform.localScale = new Vector3(1, -1, 1);
    }
}
