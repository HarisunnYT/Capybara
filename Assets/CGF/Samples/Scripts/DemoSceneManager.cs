using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DemoSceneManager : MonoBehaviour {

	public List<float> scenes = new List<float>();

	private int openSceneIndex;

	void Start () {

		openSceneIndex = SceneManager.GetActiveScene().buildIndex;

		//Debug.Log(openSceneIndex);
		
	}

	public void PreviousScene() {

		openSceneIndex = openSceneIndex - 1;

		if(openSceneIndex < 0)
		{

			SceneManager.LoadScene(SceneManager.sceneCountInBuildSettings - 1, LoadSceneMode.Single);

			openSceneIndex = SceneManager.sceneCountInBuildSettings - 1;

		}
		else {

			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1, LoadSceneMode.Single);

		}

	}

	public void NextScene() {

		openSceneIndex = openSceneIndex + 1;

		if(openSceneIndex < (SceneManager.sceneCountInBuildSettings))
		{

			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);

		}
		else {

				SceneManager.LoadScene(0, LoadSceneMode.Single);

				openSceneIndex = 0;

		}

	}

}