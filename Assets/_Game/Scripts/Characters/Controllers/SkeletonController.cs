using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonController : MonoBehaviour
{
    [SerializeField]
    private Transform[] skeletonBones;

    private CharacterController currentController;

    private void Start()
    {
        currentController = GetComponentInParent<CharacterController>();
    }

    private void Update()
    {
        for (int i = 0; i < skeletonBones.Length; i++)
        {
            skeletonBones[i].transform.position = currentController.AnimationController.MovingBones[i].position;
            skeletonBones[i].transform.localRotation = currentController.AnimationController.MovingBones[i].localRotation;
        }
    }
}
