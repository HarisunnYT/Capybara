using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : Singleton<CanvasManager>
{
    [SerializeField]
    private GameObject crosshair;

    [Space()]
    [SerializeField]
    private GameObject mobileUI;

    private void Start()
    {
        ShowCrosshair(false);

#if UNITY_ANDROID || UNITY_IOS
        mobileUI.SetActive(true);
#else 
        mobileUI.SetActive(false);
#endif
    }

    public void ShowCrosshair(bool show)
    {
        crosshair.SetActive(show);
    }
}
