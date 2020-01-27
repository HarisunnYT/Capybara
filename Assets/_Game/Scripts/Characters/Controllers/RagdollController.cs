using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RagdollController : Controller
{
    struct BoneData
    {
        public Vector3 Position;
        public Quaternion Rotation;

        public BoneData(Vector3 localPosition, Quaternion localRotation)
        {
            Position = localPosition;
            Rotation = localRotation;
        }
    }

    [SerializeField]
    private float returnFromRagdollDuration;

    [SerializeField]
    private float minRagdollTime = 2;

    [SerializeField]
    private float maxRagdollTime = 7;

    [SerializeField]
    private float requiredKnockBackForceToRagdoll = 15;
    public float RequiredKnockBackForceToRagdoll { get { return requiredKnockBackForceToRagdoll; } }

    [Space()]
    [SerializeField]
    protected Rigidbody spineBody;

    [SerializeField]
    private Rigidbody[] leftHandBones;
    public Rigidbody[] LeftHandBones { get { return leftHandBones; } }

    [SerializeField]
    private Rigidbody[] rightHandBones;
    public Rigidbody[] RightHandBones { get { return rightHandBones; } }

    [SerializeField]
    private Rigidbody[] leftLegBones;
    public Rigidbody[] LeftLegBones { get { return leftLegBones; } }

    [SerializeField]
    private Rigidbody[] rightLegBones;
    public Rigidbody[] RightLegBones { get { return rightLegBones; } }

    [SerializeField]
    private Transform metaRig;
    public Transform MetaRig { get { return metaRig; } }

    public Collider[] Colliders { get; private set; }

    private List<Rigidbody> bodies;

    private List<BoneData> lastRagdollBoneData = new List<BoneData>();

    private float timer = 0;
    private float minRagdollTimer;
    private float maxRagdollTimer;

    private bool returningFromRagdoll = false;
    private MovementState previousMovementState;

    private Collider mainCollider;

    private void Start()
    {
        bodies = gameObject.GetComponentsInChildrenExcludingRoot<Rigidbody>().ToList();
        mainCollider = GetComponent<Collider>();

        Colliders = gameObject.GetComponentsInChildrenExcludingRoot<Collider>();

        //ignore collisions between child colliders;
        foreach (var col in Colliders)
        {
            Physics.IgnoreCollision(col, mainCollider, true);
        }

        if (InteractionController.DragCharacterPart != null)
        {
            bodies.Remove(InteractionController.DragCharacterPart.Rigidbody);
        }

        SetRagdoll(false);
    }

    private void Update()
    {
        if (returningFromRagdoll)
        {
            timer += Time.deltaTime;
            float normalizedTime = timer / returnFromRagdollDuration;

            Transform[] bones = AnimationController.MovingBones;
            Transform[] animatingBones = AnimationController.AnimationBones;

            //loop through every bone and move to target
            for (int i = 0; i < bones.Length; i++)
            {
                Quaternion fromRotation = lastRagdollBoneData[i].Rotation;
                Quaternion toRotation = animatingBones[i].rotation;

                Vector3 fromPosition = lastRagdollBoneData[i].Position;
                Vector3 toPosition = animatingBones[i].transform.position;

                bones[i].rotation = Quaternion.Lerp(fromRotation, toRotation, normalizedTime);
                bones[i].position = Vector3.Lerp(fromPosition, toPosition, normalizedTime);
            }

            if (normalizedTime > 0.99f)
            {
                returningFromRagdoll = false;
                MovementController.SetMovementState(MovementState.Idle);
                AnimationController.Animator.enabled = true;

                spineBody.transform.parent = metaRig;
            }
        }
        else if ((int)MovementController.CurrentMovementState >= (int)MovementState.Ragdoll)
        {
            //check if the spine has stopped moving
            if (MovementController.CurrentMovementState != MovementState.KnockedOut)
            {
                if ((spineBody.velocity.magnitude < 0.1f && Time.time > minRagdollTimer) || Time.time > maxRagdollTimer)
                {
                    SetRagdoll(false);
                }
            }
        }
    }

    public void AddForceToBodies(Vector3 direction, float force)
    {
        foreach (var body in bodies)
        {
            body.AddForce(direction * force, ForceMode.Impulse);
        }
    }

    public void SetRagdoll(bool ragdoll, bool setVelocityFromMainBody = true)
    {
        Transform[] bones = AnimationController.MovingBones;

        //if we are trying to ragdoll and it's already ragdolling, or stop ragdoll and it isn't ragdolling, do nothing
        if ((ragdoll && MovementController.CurrentMovementState == MovementState.Ragdoll) ||
           (!ragdoll && (int)MovementController.CurrentMovementState < (int)MovementState.Ragdoll)
           || MovementController.CurrentMovementStyle == MovementStyle.Driving)
        {
            return;
        }

        //save the last ragdoll positions
        if (!ragdoll)
        {
            lastRagdollBoneData.Clear();
            for (int i = 0; i < bones.Length; i++)
            {
                lastRagdollBoneData.Add(new BoneData(bones[i].position, bones[i].rotation));
            }
        }

        foreach (var body in bodies)
        {
            body.isKinematic = !ragdoll;
        }

        timer = 0;
        returningFromRagdoll = !ragdoll;

        mainCollider.enabled = !ragdoll;
        MovementController.MainBody.isKinematic = ragdoll;

        //only disable if going into ragdoll, transition back to animation will enable animator
        if (ragdoll)
        {
            //detach from the vehicle if in one
            if (MovementController.CurrentMovementStyle == MovementStyle.Driving)
            {
                InteractionController.CurrentVehicle.GetOutOfVehicle();
            }

            spineBody.transform.parent = null;

            OnRagdollBegin();

            previousMovementState = MovementController.CurrentMovementState;

            //set spine velocity
            if (setVelocityFromMainBody)
            {
                spineBody.velocity = MovementController.MainBody.velocity;
            }

            AnimationController.Animator.enabled = false;

            MovementController.MainBody.velocity = Vector3.zero;
            MovementController.SetMovementState(MovementState.Ragdoll);

            minRagdollTimer = Time.time + minRagdollTime;
            maxRagdollTimer = Time.time + maxRagdollTime;
        }
        else
        {
            OnRagdollEnd();

            MovementController.transform.position = spineBody.transform.position;
        }
    }

    public void SetRagdollForSeconds(float seconds)
    {
        SetRagdoll(true, true);

        minRagdollTimer = Time.time + seconds;
    }

    /// <summary>
    /// Setting all the bones to kinematic or not, not using proper ragdoll system
    /// </summary>
    public void FakeRagdoll(bool ragdoll)
    {
        foreach(var rigidbody in bodies)
        {
            rigidbody.isKinematic = !ragdoll;
        }

        AnimationController.DisableAnimation(ragdoll);
    }

    public void IgnoreRagdollAgainstCollider(Collider ignoreCollider, bool ignore)
    {
        foreach (var col in Colliders)
        {
            Physics.IgnoreCollision(col, ignoreCollider, ignore);
        }
    }

    public void RagdollArms(bool ragoll)
    {
        foreach(var armBone in LeftHandBones)
        {
            armBone.isKinematic = !ragoll;
        }

        foreach (var armBone in RightHandBones)
        {
            armBone.isKinematic = !ragoll;
        }
    }

    public void RagdollLegs(bool ragoll)
    {
        foreach (var legBone in LeftLegBones)
        {
            legBone.isKinematic = !ragoll;
        }

        foreach (var legBone in RightLegBones)
        {
            legBone.isKinematic = !ragoll;
        }
    }

    public void KnockOut()
    {
        MovementController.SetMovementState(MovementState.KnockedOut);
    }

    protected virtual void OnRagdollBegin() { }
    protected virtual void OnRagdollEnd() { }

}
