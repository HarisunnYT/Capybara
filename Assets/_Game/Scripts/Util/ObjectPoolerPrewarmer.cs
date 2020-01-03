using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ObjectPoolerPrewarmer : MonoBehaviour
{
	private string path { get { return Application.persistentDataPath + "/prewarm.json"; } }

	[System.Serializable]
	public class SpawnObject
	{
		public GameObject prefab;
		public int count = 0;
	}

	public List<SpawnObject> preloadGameObjects = new List<SpawnObject>();

	[System.Serializable]
	class SaveData
	{
		public List<SpawnObject> preloadGameObjects = new List<SpawnObject>();
	}

	void Start()
	{
		StartCoroutine(Prewarm());
	}

	IEnumerator Prewarm()
	{
		List<GameObject> spawned = new List<GameObject>();

		foreach (SpawnObject obj in preloadGameObjects)
		{
			for (int i = 0; i < obj.count; i++)
				spawned.Add(ObjectPooler.GetPooledObject(obj.prefab, false));
		}

		Shader.WarmupAllShaders();

		yield return new WaitForEndOfFrame();

		foreach (GameObject obj in spawned)
		{
			obj.SetActive(false);
		}
	}

	public void Register(GameObject prefab)
	{
		bool found = false;

		foreach(SpawnObject obj in preloadGameObjects)
		{
			if(obj.prefab == prefab)
			{
				found = true;
				obj.count++;
				break;
			}
		}

		if(!found)
		{
			SpawnObject obj = new SpawnObject();
			obj.prefab = prefab;
			obj.count = 1;

			preloadGameObjects.Add(obj);
		}
	}
}
