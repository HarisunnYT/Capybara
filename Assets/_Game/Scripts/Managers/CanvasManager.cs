using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : Singleton<CanvasManager>
{
    [SerializeField]
    private GameObject crosshair;

    private void Start()
    {
        ShowCrosshair(false);
    }

    public void ShowCrosshair(bool show)
    {
        crosshair.SetActive(show);
    }
}
