using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;

    public float transitionTime = 1f;

    private void Update()
    {
        if (Application.isEditor && Input.GetKeyDown(KeyCode.R))
        {
            int currentsceneIndex = SceneManager.GetActiveScene().buildIndex;

            SceneManager.UnloadSceneAsync(currentsceneIndex);
            SceneManager.LoadScene(currentsceneIndex);
        }
    }

    public void InitLoadLevel(int levelIndex)
    {
        StartCoroutine("LoadLevel", levelIndex);
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
