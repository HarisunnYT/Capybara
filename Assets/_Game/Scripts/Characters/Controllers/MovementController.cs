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
    Idle,
    Moving,
    Stunned,


    Ragdoll = 100,

    KnockedOut = 200,
    Dead
}

public enum MovementStyle
{
    None,
    Normal,
    Flying,
    Driving,
    Dragging,

    Overriden
}

public class MovementController : Controller
{
    [SerializeField]
    protected float baseMovementSpeed = 10;
    public float BaseMovementSpeed { get { return baseMovementSpeed; } }

    [SerializeField]
    protected float maxVelocity = 100;

    [SerializeField]
    private float rotationSpeed = 5;

    [Space()]
    [SerializeField]
    protected float forwardJumpForce = 10;

    [SerializeField]
    protected float upwardJumpForce = 10;

    [SerializeField]
    private float groundedRaySize = 0.5f;

    [SerializeField]
    private LayerMask groundedLayers;

    public Rigidbody MainBody { get; private set; }
    public Collider[] Colliders { get; private set; }

    public MovementState CurrentMovementState { get; private set; }
    public MovementStyle CurrentMovementStyle { get; private set; }

    protected bool IsGrounded { get; private set; }

    protected virtual bool MoveWithRigidbody { get { return true; } }

    private Vector3 lastInputVec;

    private float knockBackSlerpDuration;
    private float jumpDelayTimer = 0;

    protected override void Awake()
    {
        base.Awake();

        MainBody = GetComponent<Rigidbody>();
        Colliders = GetComponentsInChildren<Collider>();
    }

    private void Start()
    {
        SetMovementState(MovementState.Idle);
        SetMovementStyle(MovementStyle.Normal);
    }

    private void FixedUpdate()
    {
        AnimationController.SetFloat("MovementSpeed", MainBody.velocity.magnitude);

        if ((int)CurrentMovementState < (int)MovementState.Stunned)
        {
            Move();
        }

        Vector3 gravity = GetGravity() * MainBody.mass;
        MainBody.AddForce(gravity, ForceMode.Acceleration);
    }

    protected virtual void Update()
    {
        IsGrounded = Physics.Raycast(transform.position + Vector3.up, Vector3.down, groundedRaySize, groundedLayers);
        AnimationController.ForceSetBool("InAir", !IsGrounded);
    }

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
            if (CurrentMovementStyle == MovementStyle.Normal && Time.time > knockBackSlerpDuration)
            {
                //could do sliding animation here
                MainBody.AddForce(-MainBody.velocity, ForceMode.VelocityChange);
                knockBackSlerpDuration = float.MaxValue;
            }

            if ((int)CurrentMovementState <= (int)MovementState.Moving)
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
            else if (CurrentMovementStyle == MovementStyle.Normal || CurrentMovementStyle == MovementStyle.Dragging)
            {
                //set input vector based on movement speed
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
            Vector3 inputVec = GetInputVector(inverseZAxis: true, cameraRelative: false);

            float angle = Vector3.SignedAngle(inputVec, CameraController.Instance.transform.forward, Vector3.up);
            var rotation = Quaternion.AngleAxis(angle, Vector3.up);

            rotation = Quaternion.Lerp(transform.rotation, rotation, GetRotationSpeed() * Time.deltaTime);

            MainBody.MoveRotation(rotation);
        }
        else if (CurrentMovementStyle != MovementStyle.Driving && !AimController.IsAiming)
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

    public virtual Vector3 GetInputVector(bool includeYAxis = false, bool inverseZAxis = false, bool cameraRelative = true)
    {
        return Vector3.zero;
    }

    private float GetRotationSpeed()
    {
        float rotSpeed = rotationSpeed;

        if (InteractionController.DragCharacterPart.CurrentHeldController != null)
        {
            rotSpeed /= InteractionController.DragCharacterPart.CurrentHeldController.MovementController.MainBody.mass;
        }

        return rotSpeed;
    }

    public float GetMaxVelocity()
    {
        float movementSpeed = maxVelocity;
        foreach (var bodyPart in CharacterController.BodyParts)
        {
            if (bodyPart.GetMovementData())
            {
                movementSpeed *= bodyPart.CurrentItemObject.PickupableItemData.GetMovementData(CharacterController.CharacterType).MovementSpeedMultiplier;
            }
        }

        if (InteractionController.DragCharacterPart.CurrentHeldController != null)
        {
            movementSpeed /= InteractionController.DragCharacterPart.CurrentHeldController.MovementController.MainBody.mass;
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

    protected void TryJump()
    {
        if (IsGrounded && Time.time > jumpDelayTimer)
        {
            Jump();
            jumpDelayTimer = Time.time + 0.2f;
        }
    }

    protected virtual void Jump()
    {
        MainBody.AddForce((transform.forward * forwardJumpForce) + (Vector3.up * upwardJumpForce), ForceMode.Impulse);
    }

    public void AddKnockBackForce(Vector3 direction, float force, float knockBackSlerpDuration = 1)
    {
        if (force < RagdollController.RequiredKnockBackForceToRagdoll)
        {
            if (Time.time > this.knockBackSlerpDuration || this.knockBackSlerpDuration == float.MaxValue)
            {
                MainBody.AddForce(direction * force, ForceMode.Impulse);
                this.knockBackSlerpDuration = Time.time + knockBackSlerpDuration;
            }
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

        //remove any objects that have different movement styles
        foreach(var item in InteractionController.GetCurrentItems())
        {
            MovementData data = item.PickupableItemData.GetMovementData(CharacterController.CharacterType);
            if (data && data.MovementStyle != MovementStyle.None && data.MovementStyle != movementStyle)
            {
                item.CurrentBodyPart.DropCurrentItem();
            }
        }

        CurrentMovementStyle = movementStyle;

        AnimationController.SetBool(MovementStyle.Normal.ToString(), movementStyle == MovementStyle.Normal || movementStyle == MovementStyle.Dragging);
        AnimationController.SetBool(MovementStyle.Flying.ToString(), movementStyle == MovementStyle.Flying);
    }

    public void SetKinematic(bool kinematic)
    {
        MainBody.isKinematic = kinematic;
    }

    public virtual Vector3 GetVelocity()
    {
        return MainBody.velocity;
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position + Vector3.up, (transform.position + Vector3.up) + Vector3.down * groundedRaySize);
    }
#endif
}
