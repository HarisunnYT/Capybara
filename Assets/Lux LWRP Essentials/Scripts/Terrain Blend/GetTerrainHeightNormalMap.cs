using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuxLWRPEssentials
{
	[RequireComponent (typeof (Terrain))]
	public class GetTerrainHeightNormalMap : MonoBehaviour
	{

		public TerrainData targetTerrainData;
		public string savePathTerrainHeightNormalMap;

	    public void GetTerData() {
	    	Terrain targetTerrain = (Terrain)GetComponent(typeof(Terrain));
			targetTerrainData = targetTerrain.terrainData;
	    }

	}
}