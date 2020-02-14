using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Panel : MonoBehaviour, IAnimationHandler
{
    [System.Serializable]
    enum PanelType
    {
        Normal,
        Modal
    }

    [SerializeField]
    private PanelType panelType;

    [SerializeField]
    private bool pauseTime = false;
    public bool PauseTime { get { return pauseTime; } }

    private Animator animatorGetter;
    private Animator animator
    {
        get
        {
            //lazy initialise
            if (animatorGetter == null)
                animatorGetter = GetComponent<Animator>();

            return animatorGetter;
        }
    }

    public void Close()
    {
        CanvasManager.Instance.PanelClosed(this);

        if (animator)
        {
            animator.SetTrigger("Close");
        }
        else
        {
            ObjectDisabled();
        }
    }

    public void ForceClose()
    {
        CanvasManager.Instance.PanelClosed(this);

        ObjectDisabled();
    }

    private void ObjectDisabled()
    {
        gameObject.SetActive(false);
        OnClose();
    }

    public void ShowPanel()
    {
        gameObject.SetActive(true);

        CanvasManager.Instance.PanelShown(this);

        if (panelType != PanelType.Modal)
        {
            CanvasManager.Instance.CloseAllPanels(this);
        }

        if (pauseTime)
        {
            Time.timeScale = 0;
        }

        OnShow();
    }

    protected virtual void OnClose() { }
    protected virtual void OnShow() { }

    public void OnAnimationBegin() { }

    public void OnAnimationComplete() 
    {
        ForceClose();
    }
}
