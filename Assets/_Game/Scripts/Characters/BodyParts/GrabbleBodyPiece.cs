using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbleBodyPiece : Interactable
{
    private void Start()
    {
        CurrentController = GetComponentInParent<CharacterController>();
    }
}
