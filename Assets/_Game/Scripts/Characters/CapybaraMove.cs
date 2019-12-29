using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapybaraMove : CharacterMove
{
    public static CapybaraMove Instance;

    [Space()]
    [SerializeField]
    private Transform mouthBone;

    [SerializeField]
    private CapybaraHead head;

    [SerializeField]
    private float deadzone = 0.1f;

    [Header("Interactable")]
    [SerializeField]
    private float checkRadius = 0.25f;

    private Transform rootTransform;

    private Vector3 inputAxis;
    private bool interactPressed = false;

    private IPullable currentPullable;
    private IPickupable currentPickupable;

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
                FindInteractableObjects();
            }
        }
        else if (interactPressed)
        {
            interactPressed = false;
            if (currentPullable != null)
            {
                OnPullableDropped();
            }
            else if (currentPickupable != null)
            {
                PickUpObject(currentPickupable.GetObject().transform, currentPickupable.GetOrientation(), false);
            }
        }
    }

    private void FindInteractableObjects()
    {
        Collider[] hitCols = Physics.OverlapSphere(mouthBone.transform.position, checkRadius);
        GameObject closestObject = null;

        if (hitCols.Length > 0)
        {
            for (int i = 0; i < hitCols.Length; i++)
            {
                if (hitCols[i].GetComponent(typeof(IPullable)) || hitCols[i].GetComponent(typeof(IPickupable)))
                {
                    if (closestObject == null || Vector3.Distance(hitCols[i].transform.position, mouthBone.transform.position) < Vector3.Distance(hitCols[i].transform.position, closestObject.transform.position))
                    {
                        closestObject = hitCols[i].gameObject;
                    }
                }
            }
        }

        if (closestObject != null)
        {
            IPullable pullable = closestObject.GetComponent(typeof(IPullable)) as IPullable;
            IPickupable pickupable = closestObject.GetComponent(typeof(IPickupable)) as IPickupable;
            if (pullable != null)
            {
                currentPullable = pullable;
                OnPullableBegan();
            }
            else if (pickupable != null)
            {
                currentPickupable = pickupable;
                PickUpObject(currentPickupable.GetObject().transform, currentPickupable.GetOrientation(), true);
            }
        }
    }

    private void OnPullableBegan()
    {
        currentPullable.OnPulled();

        rigidbody.isKinematic = true;

        Debug.Log("starting pulling " + currentPullable.GetObject().name);
    }

    private void OnPullableDropped()
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

    private void PickUpObject(Transform obj, Vector3 orientation, bool pickedUp)
    {
        if (pickedUp)
        {
            head.MoveToObject(currentPickupable.GetObject().transform, () =>
            {
                currentPickupable.OnPickedUp();

                obj.parent = mouthBone;
                obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.Euler(orientation);

                Debug.Log("grabbed " + currentPickupable.GetObject().name);
            });
        }
        else
        {
            obj.parent = null;

            currentPickupable.OnDropped();

            Debug.Log("dropped " + currentPickupable.GetObject().name);
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (mouthBone != null)
        {
            Gizmos.DrawWireSphere(mouthBone.transform.position, checkRadius);
        }
    }
#endif
}
