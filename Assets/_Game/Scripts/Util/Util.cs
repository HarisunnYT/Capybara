using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Axis
{
    Forward,
    NegativeForward,
    Left,
    Right,
    Up,
    Down
}

public static class Util
{
    public static bool CheckInsideLayer(LayerMask layerMask, int layer)
    {
        return layerMask == (layerMask | (1 << layer));
    }

    public static T[] GetComponentsInChildrenExcludingRoot<T>(this GameObject gameObject) where T : Component
    {
        List<T> components = gameObject.GetComponentsInChildren<T>().ToList();
        components.RemoveAt(0);

        return components.ToArray();
    }

    public static Vector3 GetDirection(Transform transform, Axis axis)
    {
        switch (axis)
        {
            case Axis.Forward:
                return transform.forward;
            case Axis.NegativeForward:
                return -transform.forward;
            case Axis.Left:
                return -transform.right;
            case Axis.Right:
                return transform.right;
            case Axis.Up:
                return transform.up;
            case Axis.Down:
                return -transform.up;
        }

        return transform.forward;
    }
}
