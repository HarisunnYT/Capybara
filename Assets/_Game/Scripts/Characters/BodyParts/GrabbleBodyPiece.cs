using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbleBodyPiece : Interactable
{
    protected override void Start()
    {
        base.Start();

        CurrentController = GetComponentInParent<CharacterController>();
    }
}
