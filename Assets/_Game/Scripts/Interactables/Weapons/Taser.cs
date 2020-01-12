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

    [Space()]
    [SerializeField]
    private float materialSwapDelay = 0.2f;
    [SerializeField]
    private Material stunMaterial;

    private CharacterController tasingCharacter;

    private SkinnedMeshRenderer meshRenderer;
    private Material originalMaterial;

    private int materialSwap = 0;
    private float stunTimer = 0;

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

    private void FixedUpdate()
    {
        if (tasingCharacter != null && Time.time > stunTimer)
        {
            stunTimer = Time.time + materialSwapDelay;

            meshRenderer.material = materialSwap == 0 ? stunMaterial : originalMaterial;
            tasingCharacter.Skeleton.gameObject.SetActive(materialSwap == 0);

            materialSwap = materialSwap == 0 ? 1 : 0;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Equiped && tasingCharacter == null && collision.gameObject.layer == LayerMask.NameToLayer("Character"))
        {
            StartCoroutine(TaseCharacter(collision.gameObject.GetComponent<CharacterController>()));
        }
    }

    private IEnumerator TaseCharacter(CharacterController character)
    {
        tasingCharacter = character;

        character.AnimationController.SetTrigger("Star", true);
        character.InteractionController.DropAllItems();

        yield return new WaitForEndOfFrame();

        character.AnimationController.DisableAllBoneLayers(true);

        foreach(var bodyPart in character.AnimationController.MovingBones)
        {
            bodyPart.transform.DOShakeRotation(stunDuration, strength, vibrato, randomness);
        }

        meshRenderer = character.GetComponentInChildren<SkinnedMeshRenderer>();
        originalMaterial = meshRenderer.material;

        yield return new WaitForSeconds(stunDuration);

        TaseFinished();
    }

    private void TaseFinished()
    {
        meshRenderer.material = originalMaterial;

        tasingCharacter.CharacterController.Skeleton.gameObject.SetActive(false);
        tasingCharacter.AnimationController.DisableAllBoneLayers(false);
        tasingCharacter = null;
    }
}
