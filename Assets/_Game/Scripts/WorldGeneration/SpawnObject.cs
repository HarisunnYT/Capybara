using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public enum ObjectType { Environment, Enemy, Building, Fence, Pickup };
    public ObjectType objectType;

    public Vector3 bounds = new Vector3(1, 0, 1);

    public Vector3 GetRotatedBounds()
    {
        Vector3 rotatedBounds = new Vector3(bounds.z, 0, bounds.x);
        return rotatedBounds;
    }

    public float MaxBounds()
    {
        return Mathf.Max(bounds.x, bounds.z);
    }
}
