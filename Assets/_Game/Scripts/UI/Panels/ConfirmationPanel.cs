using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmationPanel : Panel
{
    private System.Action yesAction;
    private System.Action noAction;

    private Panel lastPanel;

    public void ShowPanel(Panel lastPanel, System.Action yesAction, System.Action noAction)
    {
        this.yesAction = yesAction;
        this.noAction = noAction;

        this.lastPanel = lastPanel;

        ShowPanel();
    }

    public void YesPressed()
    {
        yesAction?.Invoke();

        Close();
    }

    public void NoPressed()
    {
        noAction?.Invoke();

        Close();

        if (!lastPanel.gameObject.activeSelf)
        {
            lastPanel.ShowPanel();
        }
    }
}
