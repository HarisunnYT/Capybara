using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : Panel
{
    protected override void OnShow()
    {
        GameManager.Instance.EnableCursor(true);
    }

    protected override void OnClose()
    {
        if (GameManager.Instance)
        {
            GameManager.Instance.EnableCursor(false);
        }

        CanvasManager.Instance.ClosePanel<ConfirmationPanel>();
    }

    public void QuitPressed()
    {
        CanvasManager.Instance.GetPanel<ConfirmationPanel>().ShowPanel(this, QuitToMenu, null);
    }

    private void QuitToMenu()
    {
        LevelLoader.Instance.LoadMenu();
    }
}
