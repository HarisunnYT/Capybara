using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Taser : Weapon
{
    [SerializeField]
    private float stunDuration = 2;

    [SerializeField]
    private float strength = 1;

    [SerializeField]
    private int vibrato = 10;

    [SerializeField]
    private float randomness = 90;

    private CharacterController tasingCharacter;

    public override void PickUpItem(Transform parent, BodyPart currentBodyPart, CharacterController controller)
    {
        base.PickUpItem(parent, currentBodyPart, controller);

        Rigidbody.useGravity = false;
        Rigidbody.isKinematic = false;

        Rigidbody.constraints = RigidbodyConstraints.FreezeAll;
    }

    public override void DropItem()
    {
        base.DropItem();

        Rigidbody.useGravity = true;
        Rigidbody.isKinematic = true;

        Rigidbody.constraints = RigidbodyConstraints.None;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Equiped && tasingCharacter == null && collision.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            TaseCharacter(collision.gameObject.GetComponent<CharacterController>());
        }
    }

    private void TaseCharacter(CharacterController character)
    {
        tasingCharacter = character;

        character.AnimationController.DisableAllBoneLayers(true);

        foreach(var bodyPart in character.AnimationController.MovingBones)
        {
            bodyPart.transform.DOShakeRotation(stunDuration, strength, vibrato, randomness);
        }

        Invoke("TaseFinished", stunDuration);
    }

    private void TaseFinished()
    {
        tasingCharacter.AnimationController.DisableAllBoneLayers(false);
        tasingCharacter = null;
    }
}
