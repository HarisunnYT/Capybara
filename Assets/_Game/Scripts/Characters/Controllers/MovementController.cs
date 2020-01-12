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
    EitherHand,
    BackLegs,
    Bum,
}

public enum MovementState
{
    Moving,
    Idle,
    Stunned,
    Ragdoll,
    KnockedOut,
}

public enum MovementStyle
{
    None,
    Grounded,
    Flying,
    Driving,
    Dragging
}

public class MovementController : Controller
{
    [SerializeField]
    protected float baseMovementSpeed = 10;

    [SerializeField]
    protected float maxVelocity = 100;

    [SerializeField]
    private float rotationSpeed = 5;

    public Rigidbody MainBody { get; private set; }
    public Collider[] Colliders { get; private set; }

    public MovementState CurrentMovementState { get; private set; }
    public MovementStyle CurrentMovementStyle { get; private set; }

    protected virtual bool MoveWithRigidbody { get { return true; } }

    private Vector3 lastInputVec;

    private float knockBackSlerpDuration;

    protected override void Awake()
    {
        base.Awake();

        MainBody = GetComponent<Rigidbody>();
        Colliders = GetComponentsInChildren<Collider>();
    }

    private void Start()
    {
        SetMovementState(MovementState.Idle);
        SetMovementStyle(MovementStyle.Grounded);
    }

    private void FixedUpdate()
    {
        AnimationController.SetFloat("MovementSpeed", MainBody.velocity.magnitude);

        if (CurrentMovementState != MovementState.Ragdoll)
        {
            Move();
        }

        Vector3 gravity = GetGravity() * MainBody.mass;
        MainBody.AddForce(gravity, ForceMode.Acceleration);
    }

    #region Movement

    private void Move()
    {
        Vector3 inputVec = GetInputVector(true);
        if (inputVec.x != 0 || inputVec.z != 0)
        {
            if ((int)CurrentMovementState < 2)
            {
                DoRotation();

                lastInputVec = inputVec;
                SetMovementState(MovementState.Moving);
            }
        }
        else
        {
            if (CurrentMovementStyle == MovementStyle.Grounded && Time.time > knockBackSlerpDuration)
            {
                //could do sliding animation here
                MainBody.AddForce(-MainBody.velocity, ForceMode.VelocityChange);
                knockBackSlerpDuration = float.MaxValue;
            }

            if ((int)CurrentMovementState < 2)
            {
                SetMovementState(MovementState.Idle);
            }
        }

        float movementSpeed = GetMaxVelocity();

        Vector3 movementVector = inputVec;
        movementVector.y = 0;

        float fixedDTime = Time.fixedDeltaTime;

        //movement style specific
        if (MoveWithRigidbody)
        {
            if (CurrentMovementStyle == MovementStyle.Flying)
            {
                movementVector = new Vector3(movementVector.x * movementSpeed * fixedDTime, inputVec.y * movementSpeed * fixedDTime, movementVector.z * movementSpeed * fixedDTime);
                MainBody.velocity = movementVector;
            }
            else if (CurrentMovementStyle == MovementStyle.Grounded)
            {
                //set input vector based on movement speed
                movementVector = new Vector3(movementVector.x * baseMovementSpeed * fixedDTime, 0, movementVector.z * baseMovementSpeed * fixedDTime);
                if (MainBody.velocity.magnitude < GetMaxVelocity())
                {
                    MainBody.AddForce(movementVector, ForceMode.VelocityChange);
                }
            }
            else if (CurrentMovementStyle == MovementStyle.Dragging)
            {
                movementVector = new Vector3(movementVector.x * baseMovementSpeed * fixedDTime, 0, movementVector.z * baseMovementSpeed * fixedDTime);
                if (MainBody.velocity.magnitude < GetMaxVelocity())
                {
                    MainBody.AddForce(movementVector, ForceMode.VelocityChange);
                }
            }
        }
    }

    private void DoRotation()
    {
        if (CurrentMovementStyle == MovementStyle.Dragging)
        {
            Quaternion cameraRotation = CameraController.Instance.transform.rotation;
            Vector3 angle = new Vector3(0, 5, 0);

            transform.RotateAround(InteractionController.Mouth.CurrentHeldBone.transform.position, Vector3.up, 1);
        }
        else if (CurrentMovementStyle != MovementStyle.Driving)
        {
            transform.rotation = GetRotation();
        }
    }

    private Quaternion GetRotation()
    {
        if (lastInputVec == Vector3.zero)
        {
            return transform.rotation;
        }

        Quaternion toRotation = Quaternion.LookRotation(lastInputVec, Vector3.up);
        Quaternion rotation = Quaternion.Lerp(transform.rotation, toRotation, GetRotationSpeed() * Time.deltaTime);

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

    public virtual Vector3 GetInputVector(bool includeYAxis = false)
    {
        return Vector3.zero;
    }

    private float GetRotationSpeed()
    {
        float rotSpeed = rotationSpeed;

        if (InteractionController.Mouth.CurrentHeldController != null)
        {
            rotSpeed /= InteractionController.Mouth.CurrentHeldController.MovementController.MainBody.mass;
        }

        return rotSpeed;
    }

    private float GetMaxVelocity()
    {
        float movementSpeed = maxVelocity;
        foreach (var bodyPart in CharacterController.BodyParts)
        {
            if (bodyPart.GetMovementData())
            {
                movementSpeed *= bodyPart.CurrentItemObject.PickupableItemData.GetMovementData(CharacterController.CharacterType).MovementSpeedMultiplier;
            }
        }

        if (InteractionController.Mouth.CurrentHeldController != null)
        {
            movementSpeed /= InteractionController.Mouth.CurrentHeldController.MovementController.MainBody.mass;
        }

        return movementSpeed;
    }

    private Vector3 GetGravity()
    {
        Vector3 gravity = new Vector3(1, Physics.gravity.y, 1);
        bool modifiedGravity = false;

        foreach (var bodyPart in CharacterController.BodyParts)
        {
            if (bodyPart.GetMovementData())
            {
                Vector3 multiplier = bodyPart.CurrentItemObject.PickupableItemData.GetMovementData(CharacterController.CharacterType).GravityMultiplier;
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

    public void AddKnockBackForce(Vector3 direction, float force, float knockBackSlerpDuration = 1)
    {
        if (force < RagdollController.RequiredKnockBackForceToRagdoll)
        {
            MainBody.AddForce(direction * force, ForceMode.Impulse);
            this.knockBackSlerpDuration = Time.time + knockBackSlerpDuration;
        }
        if (force > RagdollController.RequiredKnockBackForceToRagdoll)
        {
            RagdollController.SetRagdoll(true, false);
            RagdollController.AddForceToBodies(direction, force);
        }
    }

    public void SetMovementState(MovementState movementState)
    {
        AnimationController.SetBool(CurrentMovementState.ToString(), false);

        CurrentMovementState = movementState;

        AnimationController.SetBool(movementState.ToString(), true);
    }

    public void SetMovementStyle(MovementStyle movementStyle)
    {
        if (movementStyle == MovementStyle.None)
        {
            return;
        }

        CurrentMovementStyle = movementStyle;

        AnimationController.SetBool(MovementStyle.Grounded.ToString(), movementStyle == MovementStyle.Grounded || movementStyle == MovementStyle.Dragging);
        AnimationController.SetBool(MovementStyle.Flying.ToString(), movementStyle == MovementStyle.Flying);
        AnimationController.SetBool(MovementStyle.Driving.ToString(), movementStyle == MovementStyle.Driving);
    }

    public void SetKinematic(bool kinematic)
    {
        MainBody.isKinematic = kinematic;
    }

    #endregion
}
