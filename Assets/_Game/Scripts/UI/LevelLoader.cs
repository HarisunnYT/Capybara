using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : Singleton<LevelLoader>
{
    public Animator transition;

    public float transitionTime = 1f;

    public const int MenuSceneIndex = 0;
    public const int GameSceneIndex = 1;

    private void Update()
    {
        if (Application.isEditor && Input.GetKeyDown(KeyCode.R))
        {
            int currentsceneIndex = SceneManager.GetActiveScene().buildIndex;

            SceneManager.UnloadSceneAsync(currentsceneIndex);
            SceneManager.LoadScene(currentsceneIndex);
        }
    }

    public int GetCurrentSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public void LoadMenu()
    {
        StartCoroutine(UnloadSceneAsync(GameSceneIndex, afterFullTransition: () =>
        {
            CanvasManager.Instance.ShowPanel<MainMenuPanel>();
        }));
    }

    public void LoadGame()
    {
        StartCoroutine(LoadSceneAsync(GameSceneIndex, afterFullTransition: () =>
        {
            CanvasManager.Instance.ShowPanel<HUDPanel>();
        }));
    }

    private IEnumerator UnloadSceneAsync(int sceneIndex, System.Action afterInitialTransition = null, System.Action afterFullTransition = null)
    {
        CanvasManager.Instance.ShowPanel<TransitionPanel>();

        //we need to wait at least a second for the panel to animate
        yield return new WaitForSecondsRealtime(1);

        AsyncOperation levelAsync = SceneManager.UnloadSceneAsync(sceneIndex);
        levelAsync.allowSceneActivation = false;

        afterInitialTransition?.Invoke();

        while (levelAsync.progress < 0.9f)
        {
            yield return new WaitForEndOfFrame();
        }

        levelAsync.allowSceneActivation = true;

        //stutter fix
        yield return new WaitForSecondsRealtime(0.5f);

        CanvasManager.Instance.ClosePanel<TransitionPanel>();
        CanvasManager.Instance.CloseAllPanels(CanvasManager.Instance.GetPanel<TransitionPanel>());

        afterFullTransition?.Invoke();
    }

    private IEnumerator LoadSceneAsync(int sceneIndex, System.Action afterInitialTransition = null, System.Action afterFullTransition = null)
    {
        CanvasManager.Instance.ShowPanel<TransitionPanel>();

        //we need to wait at least a second for the panel to animate
        yield return new WaitForSecondsRealtime(1);

        AsyncOperation levelAsync = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        levelAsync.allowSceneActivation = false;

        afterInitialTransition?.Invoke();

        while (levelAsync.progress < 0.9f)
        {
            yield return new WaitForEndOfFrame();
        }

        levelAsync.allowSceneActivation = true;

        //stutter fix
        yield return new WaitForSecondsRealtime(0.5f);

        CanvasManager.Instance.ClosePanel<TransitionPanel>();
        CanvasManager.Instance.CloseAllPanels(CanvasManager.Instance.GetPanel<TransitionPanel>());

        afterFullTransition?.Invoke();
    }
}
