using System.Collections;
using System.Collections.Generic;   
using UnityEngine;

public class WorldSeed : MonoBehaviour
{
    [SerializeField]
    private string strSeed;

    [SerializeField]
    private int seed;

    [SerializeField]
    private int maxSeed = 99999999;

    void Awake()
    {
        if (strSeed.Length >= 3)
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
