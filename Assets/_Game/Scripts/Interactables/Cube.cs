using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour, IPullable
{
    public GameObject GetObject()
    {
        return gameObject;
    }

    public void OnDropped()
    {
       
    }

    public void OnPulled()
    {
        
    }
}
