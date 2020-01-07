using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Node
{
    public string name;

    public Vector3 pos;
    public bool used;
    public bool path;
    public bool enclosure;

    public float distanceToDest;
    public Node cameFromNode;

    public int gCost = int.MaxValue;
    public int hCost = int.MaxValue;
    public int fCost = int.MaxValue;

    public void CalculateFCost()
    {
        fCost = gCost + hCost;
    }
}
