using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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
}
