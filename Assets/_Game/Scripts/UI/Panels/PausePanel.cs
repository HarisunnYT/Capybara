using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : Panel
{
    public void QuitPressed()
    {
        CanvasManager.Instance.GetPanel<ConfirmationPanel>().ShowPanel(this, QuitToMenu, () =>
        {
            CanvasManager.Instance.ClosePanel<ConfirmationPanel>();
        });
    }

    private void QuitToMenu()
    {
        LevelLoader.Instance.LoadMenu();
    }
}
