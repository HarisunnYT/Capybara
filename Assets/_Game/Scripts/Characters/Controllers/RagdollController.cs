using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : Controller
{
    struct BoneData
    {
        public Vector3 Position;
        public Quaternion Rotation;

        public BoneData (Vector3 localPosition, Quaternion localRotation)
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
    private float requiredKnockBackForceToRagdoll = 15;
    public float RequiredKnockBackForceToRagdoll { get { return requiredKnockBackForceToRagdoll; } }

    [Space()]
    [SerializeField]
    protected Rigidbody spineBody;

    [SerializeField]
    private Transform metaRig;

    private Rigidbody[] bodies;

    private List<BoneData> lastRagdollBoneData = new List<BoneData>();

    private float timer = 0;
    private float ragdollTime;

    private bool returningFromRagdoll = false;
    private MovementState previousMovementState;

    private void Start()
    {
        bodies = gameObject.GetComponentsInChildrenExcludingRoot<Rigidbody>();

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
                MovementController.SetMovementState(previousMovementState);
                AnimationController.Animator.enabled = true;

                spineBody.transform.parent = metaRig;
            }
        }
        else if (MovementController.CurrentMovementState == MovementState.Ragdoll)
        {
            //check if the spine has stopped moving
            if (spineBody.velocity.magnitude < 0.1f && Time.time > ragdollTime)
            {
                SetRagdoll(false);
            }
        }
    }

    public void AddForceToBodies(Vector3 direction, float force)
    {
        foreach(var body in bodies)
        {
            body.AddForce(direction * force, ForceMode.Impulse);
        }
    }

    public void SetRagdoll(bool ragdoll, bool setVelocityFromMainBody = true)
    {
        Transform[] bones = AnimationController.MovingBones;

        //if we are trying to ragdoll and it's already ragdolling, or stop ragdoll and it isn't ragdolling, do nothing
        if ((ragdoll && MovementController.CurrentMovementState == MovementState.Ragdoll) ||
           (!ragdoll && MovementController.CurrentMovementState != MovementState.Ragdoll))
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

        //only disable if going into ragdoll, transition back to animation will enable animator
        if (ragdoll)
        {
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

            ragdollTime = Time.time + minRagdollTime;
        }
        else
        {
            OnRagdollEnd();

            MovementController.transform.position = spineBody.transform.position;
        }
    }

    protected virtual void OnRagdollBegin() { }
    protected virtual void OnRagdollEnd() { }

}
