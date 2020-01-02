using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    [SerializeField]
    private Hand leftHand;

    [SerializeField]
    private Hand rightHand;

    [SerializeField]
    private Hand twoHand;

    private void Update()
    {
        if (InputController.InputManager.Attack.WasPressed)
        {
            //if the two hand can't attack (eg no weapon) try attacking with single hands instead
            if (twoHand.Attack() || leftHand.Attack() || rightHand.Attack()) 
            {
                AnimationController.Instance.SetTrigger("Attack");
            }
        }
    }
}
