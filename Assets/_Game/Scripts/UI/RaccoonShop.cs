using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaccoonShop : MonoBehaviour
{
    private const int shopUseDist = 3;

    private GameObject raccoonShop;

    // all shitty placeholder code 
    private void Start()
    {
        raccoonShop = GameObject.Find("RaccoonShop");
    }

    private void Update()
    {
        if (WorldGenerator.Instance.spawnedPlayer != null)
        {
            if (Vector3.Distance(gameObject.transform.position, WorldGenerator.Instance.spawnedPlayer.transform.Find("Capybara Body").position) < shopUseDist)
            {
                raccoonShop.SetActive(true);
            }
            else if (raccoonShop != null)
            {
                raccoonShop.SetActive(false);
            }
        }
    }
}
