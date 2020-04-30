using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using DG.Tweening;

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

    public static void PunchIntigerCounter(TMP_Text text, float duration, int previousAmount, int newAmount)
    {
        int counter = previousAmount;
        float punchDuration = duration / (newAmount - previousAmount);

        DOTween.To(() => counter, x => counter = x, newAmount, duration).OnUpdate(() =>
        {
            text.text = counter.ToString();
        });
    }

    public static T[] GetItemsOfType<T>(string path) where T : ScriptableObject
    {
        return Resources.LoadAll<T>(path);
    }

    public static float ConvertRange(float previousValue, float previousMin, float previousMax, float newMin, float newMax)
    {
        float previousRange = previousMax - previousMin;

        if (System.Math.Abs(previousRange) < Mathf.Epsilon)
            return newMin;

        float newRange = newMax - newMin;
        float newValue = (((previousValue - previousMin) * newRange) / previousRange) + newMin;
        return newValue;
    }

}
