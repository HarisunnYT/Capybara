using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyPartType
{
    Head,
    Neck,
    Torso,
    Mouth,
    TwoHand,
    LeftHand,
    RightHand,
    BackLegs,
    Bum
}

public enum MovementState
{
    Moving,
    Idle,
    Ragdoll
}

public enum MovementStyle
{
    None,
    Grounded,
    Flying
}

public class CapybaraController : MonoBehaviour
{
    public static CapybaraController Instance;

    [SerializeField]
    private float baseMovementSpeed = 10;

    [SerializeField]
    private float rotationSpeed = 5;

    public Rigidbody MainBody { get; private set; }
    public Animator Animator { get; private set; }

    public BodyPart[] BodyParts { get; private set; }
    public Collider[] Colliders { get; private set; }

    public MovementState CurrentMovementState { get; private set; }
    public MovementStyle CurrentMovementStyle { get; private set; }

    private Vector3 lastInputVec;

    private void Awake()
    {
        Instance = this;

        MainBody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();

        BodyParts = GetComponentsInChildren<BodyPart>();
        Colliders = GetComponentsInChildren<Collider>();

        SetMovementState(MovementState.Idle);
    }

    private void Update()
    {
        if (CurrentMovementState != MovementState.Ragdoll)
        {
            Move();
        }
    }

    #region Movement

    private void Move()
    {
        Vector3 inputVec = GetInputVector();
        Vector3 gravity = GetGravity();

        inputVec = GetMovement();

        if (inputVec.x != 0 || inputVec.z != 0)
        {
            lastInputVec = inputVec;

            SetMovementState(MovementState.Moving);
        }
        else
        {
            SetMovementState(MovementState.Idle);
        }

        transform.rotation = GetRotation();

        //move rigidbody
        MainBody.velocity = inputVec;

        //apply gravity multiplier
        MainBody.AddForce(gravity, ForceMode.Acceleration);
    }

    private Vector3 GetMovement()
    {
        Vector3 inputVec = GetInputVector();
        float movementSpeed = GetMovementSpeed();

        Vector3 movementVector = inputVec;
        movementVector.y = MainBody.velocity.y;

        //movement style specific
        if (CurrentMovementStyle == MovementStyle.Flying)
        {
            movementVector = new Vector3(movementVector.x * movementSpeed, inputVec.y * movementSpeed, movementVector.z * movementSpeed);
        }
        else
        {
            //set input vector based on movement speed
            movementVector = new Vector3(movementVector.x * movementSpeed, movementVector.y, movementVector.z * movementSpeed);
        }

        return movementVector;
    }

    private Quaternion GetRotation()
    {
        if (lastInputVec == Vector3.zero)
        {
            return transform.rotation;
        }

        Quaternion toRotation = Quaternion.LookRotation(lastInputVec, Vector3.up);
        Quaternion rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        if (CurrentMovementStyle == MovementStyle.Flying)
        {
            rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, 0);
        }
        else
        {
            rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, 0);
        }

        return rotation;
    }

    private Vector3 GetInputVector()
    {
        Vector3 inputVec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        inputVec = CameraController.Instance.transform.TransformDirection(inputVec);

        return inputVec;
    }

    private float GetMovementSpeed()
    {
        float movementSpeed = baseMovementSpeed;
        foreach(var bodyPart in BodyParts)
        {
            if (bodyPart.GetMovementData())
            {
                movementSpeed *= bodyPart.CurrentItemObject.PickupableItemData.MovementData.MovementSpeedMultiplier;
            }
        }

        return movementSpeed;
    }

    private Vector3 GetGravity()
    {
        Vector3 gravity = new Vector3(1, Physics.gravity.y, 1);
        bool modifiedGravity = false;

        foreach (var bodyPart in BodyParts)
        {
            if (bodyPart.GetMovementData())
            {
                Vector3 multiplier = bodyPart.CurrentItemObject.PickupableItemData.MovementData.GravityMultiplier;
                gravity = new Vector3(gravity.x * multiplier.x, gravity.y * multiplier.y, gravity.z * multiplier.z);

                modifiedGravity = true;
            }
        }

        //if the gravity wasn't modified, set the x and z axis back to 0
        if (!modifiedGravity)
        {
            gravity = new Vector3(0, gravity.y, 0);
        }

        return gravity;
    }

    public void SetMovementState(MovementState movementState)
    {
        AnimationController.Instance.SetBool(CurrentMovementState.ToString(), false);

        CurrentMovementState = movementState;

        AnimationController.Instance.SetBool(movementState.ToString(), true);
    }

    public void SetMovementStyle(MovementStyle movementStyle)
    {
        CurrentMovementStyle = movementStyle;

        Animator.SetBool(MovementStyle.Flying.ToString(), movementStyle == MovementStyle.Flying);
        Animator.SetBool(MovementStyle.Grounded.ToString(), movementStyle == MovementStyle.Grounded);
    }

    #endregion
}
