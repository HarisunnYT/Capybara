using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuxLWRPEssentials
{
	public class DecalManager : MonoBehaviour
	{
		public bool Gizmos = true;
		public static bool DrawDecalGizmos = true;

		void OnValidate() {
			DrawDecalGizmos = Gizmos;
		}
	}
}