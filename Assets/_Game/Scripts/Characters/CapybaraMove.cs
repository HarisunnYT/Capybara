using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapybaraMove : CharacterMove
{
    [Space()]
    [SerializeField]
    private Transform headBone;

    [SerializeField]
    private float deadzone = 0.1f;

    [Header("Pullable")]
    [SerializeField]
    private float checkRadius = 0.25f;

    private Vector3 inputAxis;
    private bool interactPressed = false;

    private IPullable currentPullable;

    private void Update()
    {
        inputAxis = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (inputAxis != Vector3.zero && inputAxis.magnitude > deadzone)
        {
            Vector3 newInputAxis = CameraController.Instance.transform.TransformDirection(inputAxis);
            newInputAxis.y = 0;

            //pulling object
            if (currentPullable != null)
            {
                if (inputAxis.x < 0)
                {
                    SetWalkingBackwards(inputAxis, true);
                }
                else
                {
                    SetWalking(inputAxis, true);
                }
            }
            else
            {
                if (newInputAxis.magnitude > 0.5f)
                {
                    SetRunning(newInputAxis);
                }
                else
                {
                    SetWalking(newInputAxis);
                }
            }
        }
        else
        {
            SetIdle();
        }

        if (Input.GetAxisRaw("Interact") != 0)
        {
            if (!interactPressed)
            {
                interactPressed = true;
                FindPullableObject();
            }
        }
        else if (interactPressed)
        {
            interactPressed = false;
            if (currentPullable != null)
            {
                OnDrop();
            }
        }
    }

    private void FindPullableObject()
    {
        Collider[] hitCols = Physics.OverlapSphere(headBone.transform.position, checkRadius);
        GameObject pullableObject = null;

        if (hitCols.Length > 0)
        {
            for (int i = 0; i < hitCols.Length; i++)
            {
                if (hitCols[i].GetComponent(typeof(IPullable)))
                {
                    if (pullableObject == null || Vector3.Distance(hitCols[i].transform.position, headBone.transform.position) < Vector3.Distance(hitCols[i].transform.position, pullableObject.transform.position))
                    {
                        pullableObject = hitCols[i].gameObject;
                    }
                }
            }
        }

        if (pullableObject != null)
        {
            IPullable pullable = pullableObject.GetComponent(typeof(IPullable)) as IPullable;
            currentPullable = pullable;

            OnPull();
        }
    }

    private void OnPull()
    {
        currentPullable.OnPulled();
        currentPullable.GetObject().GetComponent<SpringJoint>().connectedBody = headBone.GetComponent<Rigidbody>();

        Debug.Log("starting pulling " + currentPullable.GetObject().name);
    }

    private void OnDrop()
    {
        Debug.Log("dropped " + currentPullable.GetObject().name);

        currentPullable.GetObject().GetComponent<SpringJoint>().connectedBody = null;

        currentPullable.OnDropped();
        currentPullable = null;
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (headBone != null)
        {
            Gizmos.DrawWireSphere(headBone.transform.position, checkRadius);
        }
    }
#endif
}
