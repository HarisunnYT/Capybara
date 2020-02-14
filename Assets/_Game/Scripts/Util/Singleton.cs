using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance { get { return instance; } }

    protected void Awake()
    {
        Register();
    }

    protected virtual void Register()
    {
        if (instance == null)
        {
            instance = this as T;

            Initialize();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    protected virtual void OnDestroy()
    {
        if (instance == this)
        {
            Deinitialize();
            instance = null;
        }
    }

    protected virtual void Initialize() { }

    protected virtual void Deinitialize() { }
}
