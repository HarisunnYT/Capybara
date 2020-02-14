using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDPanel : Panel
{
    [SerializeField]
    private GameObject crosshair; 

    public void ShowCrosshair(bool show)
    {
        crosshair.SetActive(show);
    }
}
