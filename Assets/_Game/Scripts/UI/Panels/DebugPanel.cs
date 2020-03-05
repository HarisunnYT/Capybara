using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPanel : Panel
{
    public void TakeDamagedSingle()
    {
        GameManager.Instance.CapyController.HealthController.Damaged(1);
    }

    public void CinematicMode()
    {
        if (CanvasManager.Instance.GetPanel<HUDPanel>().gameObject.activeSelf)
        {
            CanvasManager.Instance.ClosePanel<HUDPanel>();
        }
        else
        {
            CanvasManager.Instance.ShowPanel<HUDPanel>();
        }
    }

    public void AddThousandCoins()
    {
        CurrencyManager.Instance.AddCurrency(1000);
    }

    public void ClearCoins()
    {
        CurrencyManager.Instance.ClearCurrency();
    }
}
