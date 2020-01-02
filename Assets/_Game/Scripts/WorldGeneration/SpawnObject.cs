using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    public enum ObjectType { Environment, Enemy, Building, Fence, Pickup };
    public ObjectType objectType;

    public int bounds;
}
