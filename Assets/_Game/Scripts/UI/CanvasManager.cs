using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : Singleton<CanvasManager>
{
    private Panel[] panels;

    private int timeScaleCounter = 0;

    protected override void Initialize()
    {
        panels = transform.GetComponentsInChildren<Panel>(true);
        
        //if (LevelLoader.Instance.GetCurrentSceneIndex() == LevelLoader.MenuSceneIndex)
        //{
        //    ShowPanel<MainMenuPanel>();
        //}
        //else if (LevelLoader.Instance.GetCurrentSceneIndex() == LevelLoader.GameSceneIndex)
        //{
            ShowPanel<HUDPanel>();
        //}
    }

    private void Update()
    {
        if (LevelLoader.Instance.CurrentSceneIndex == LevelLoader.GameSceneIndex)
        {
            if (InputController.InputManager.Escape.WasPressed)
            {
                if (GetPanel<PausePanel>().gameObject.activeSelf)
                {
                    ClosePanel<PausePanel>();
                }
                else
                {
                    ShowPanel<PausePanel>();
                }
            }
        }
    }

    public void ShowPanel<T>() where T : Panel
    {
        GetPanel<T>().ShowPanel();
    }

    public T GetPanel<T>() where T : Panel
    {
        foreach(var panel in panels)
        {
            if (panel is T)
                return panel as T;
        }

        return null;
    }

    public void ClosePanel<T>() where T : Panel
    {
        GetPanel<T>().Close();
    }

    public void ForceClosePanel<T>() where T : Panel
    {
        GetPanel<T>().ForceClose();
    }

    public void CloseAllPanels(Panel leaveOpenPanel = null)
    {
        foreach (var panel in panels)
        {
            if (leaveOpenPanel == null || panel != leaveOpenPanel)
            {
                panel.Close();
            }
        }
    }

    public void PanelShown(Panel panel)
    {
        if (panel.PauseTime)
        {
            timeScaleCounter++;

            Time.timeScale = 0;
        }
    }

    public void PanelClosed(Panel panel)
    {
        if (panel.PauseTime)
        {
            timeScaleCounter--;
            timeScaleCounter = Mathf.Clamp(timeScaleCounter, 0, int.MaxValue);

            if (timeScaleCounter <= 0)
            {
                Time.timeScale = 1;
            }
        }
    }
}
