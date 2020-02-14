using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuPanel : Panel
{

    public void LoadGameScene()
    {
        LevelLoader.Instance.LoadGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
