using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapybaraMove : CharacterMove
{
    public static CapybaraMove Instance;

    [Space()]
    [SerializeField]
    private Transform headBone;

    [SerializeField]
    private float deadzone = 0.1f;

    [Header("Pullable")]
    [SerializeField]
    private float checkRadius = 0.25f;

    private Transform rootTransform;

    private Vector3 inputAxis;
    private bool interactPressed = false;

    private IPullable currentPullable;

    public Vector3 InputAxis { get { return new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")); } }
    public Collider Collider { get; private set; }

    protected override void Start()
    {
        Instance = this;
        rootTransform = transform.parent;

        Collider = transform.GetComponent<Collider>();

        base.Start();
    }

    private void Update()
    {
        inputAxis = InputAxis;
        if (inputAxis != Vector3.zero && inputAxis.magnitude > deadzone)
        {
            Vector3 newInputAxis = CameraController.Instance.transform.TransformDirection(inputAxis);
            newInputAxis.y = 0;

            //pulling object
            if (currentPullable != null)
            {
                Vector3 modelAxis = transform.TransformDirection(inputAxis);
                if (modelAxis.x < 0)
                {
                    SetAnimation(AnimationState.Walk, newInputAxis, true);
                }
                else
                {
                    SetAnimation(AnimationState.WalkBackwards, newInputAxis, true);
                }
            }
            else
            {
                if (newInputAxis.magnitude > 0.5f)
                {
                    SetAnimation(AnimationState.Run, newInputAxis);
                }
                else
                {
                    SetAnimation(AnimationState.Walk, newInputAxis);
                }
            }
        }
        else
        {
            SetAnimation(AnimationState.Idle, Vector3.zero);
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

        rigidbody.isKinematic = true;

        Debug.Log("starting pulling " + currentPullable.GetObject().name);
    }

    private void OnDrop()
    {
        Debug.Log("dropped " + currentPullable.GetObject().name);

        rigidbody.isKinematic = false;

        currentPullable.OnDropped();
        currentPullable = null;
    }

    public void SetParent(Transform parent)
    {
        rootTransform.parent = parent;
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
