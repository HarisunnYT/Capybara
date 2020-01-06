using System.Collections;
using System.Collections.Generic;   
using UnityEngine;

public class WorldSeed : MonoBehaviour
{
    public static WorldSeed instance;

    [SerializeField]
    private string strSeed;

    public int seed;

    [SerializeField]
    private int maxSeed = 99999999;

    void Awake()
    {
        instance = this;
        SetSeed();
    }

    public void SetSeed(bool forceRandom = false)
    {
        if (strSeed.Length >= 3 && !forceRandom)
        {
            seed = strSeed.GetHashCode();
        }
        else
        {
            seed = Random.Range(0, maxSeed);
        }

        Random.InitState(seed);
    }
}
