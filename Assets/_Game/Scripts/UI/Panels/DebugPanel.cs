using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugPanel : Panel
{
    public void TakeDamagedSingle()
    {
        GameManager.Instance.CapyController.HealthController.Damaged(1);
    }
}
