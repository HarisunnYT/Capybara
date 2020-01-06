using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public Vector3 pos;
    public bool used;
    public bool path;
    public bool enclosure;

    public float distanceToDest;
}
