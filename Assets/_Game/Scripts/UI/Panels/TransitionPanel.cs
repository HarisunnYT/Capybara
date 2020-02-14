using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionPanel : Panel
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
}
