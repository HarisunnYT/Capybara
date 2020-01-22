using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIInteractionController : InteractionController
{
    public bool CanGrabCapy(float radius = 1)
    {
        return FindCapyBodyPart(radius) != null;
    }

    public bool TryGrabCapy(float radius = 1)
    {
        Interactable interactable = FindCapyBodyPart(radius);
        if (interactable)
        {
            interactable.CurrentController.RagdollController.SetRagdoll(true);

            DragCharacterPart.GrabRagdoll(interactable as GrabbleBodyPiece);
        }

        return interactable != null;
    }

    public void DropCapy()
    {
        DragCharacterPart.DropRagdoll();
    }

    private Interactable FindCapyBodyPart(float radius = 1)
    {
        if (MovementController == null)
        {
            return null;
        }

        Collider[] hitCols = Physics.OverlapSphere(transform.position, radius);
        Interactable closestObject = null;

        if (hitCols.Length > 0)
        {
            for (int i = 0; i < hitCols.Length; i++)
            {
                Interactable item = hitCols[i].GetComponent<Interactable>();
                if (item != null && !item.Equiped)
                {
                    if (closestObject == null || Vector3.Distance(hitCols[i].transform.position, transform.position) < Vector3.Distance(hitCols[i].transform.position, closestObject.transform.position))
                    {
                        if (item is GrabbleBodyPiece && item.CurrentController == GameManager.Instance.CapyController)
                        {
                            closestObject = item;
                        }
                    }
                }
            }
        }

        if (closestObject != null)
        {
            return closestObject;
        }

        return null;
    }
}
