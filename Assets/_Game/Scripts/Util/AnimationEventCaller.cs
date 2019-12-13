using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventCaller : MonoBehaviour
{
    [SerializeField]
    private MonoBehaviour scriptToCall = null;

    public void CallFunction(string functionName)
    {
        scriptToCall.Invoke(functionName, 0);
    }
}
