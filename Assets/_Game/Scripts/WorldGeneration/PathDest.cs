using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDest : MonoBehaviour
{
    private void Awake()
    {
        NodeManager.Instance.pathDests.Add(NodeManager.Instance.GetNodeAtPosition(transform.position));
    }
}
