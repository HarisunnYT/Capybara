using UnityEngine;
using System.Collections;

public class LogicGlobal : MonoBehaviour
{
    void Start()
    {

    }

    public static void GlobalGUI()
    {
        GUILayout.Label("Press 1-4 to select different sample scenes");
        GUILayout.Space(20);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1)) UnityEngine.SceneManagement.SceneManager.LoadScene(0);
        if(Input.GetKeyDown(KeyCode.Alpha2)) UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        if(Input.GetKeyDown(KeyCode.Alpha3)) UnityEngine.SceneManagement.SceneManager.LoadScene(2);
        if(Input.GetKeyDown(KeyCode.Alpha4)) UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }
}
