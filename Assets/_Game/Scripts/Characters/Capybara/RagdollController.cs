using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
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

    public static RagdollController Instance;

    [SerializeField]
    private float returnFromRagdollDuration;

    [SerializeField]
    private float minRagdollTime = 2;

    [SerializeField]
    private float requiredKnockBackForceToRagdoll = 15;
    public float RequiredKnockBackForceToRagdoll { get { return requiredKnockBackForceToRagdoll; } }

    [Space()]
    [SerializeField]
    private Rigidbody spineBody;

    [SerializeField]
    private Transform metaRig;

    private Rigidbody[] bodies;

    private List<BoneData> lastRagdollBoneData = new List<BoneData>();

    private float timer = 0;
    private float ragdollTime;

    private bool returningFromRagdoll = false;
    private MovementState previousMovementState;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        bodies = gameObject.GetComponentsInChildrenExcludingRoot<Rigidbody>();
    }

    private void Update()
    {
        if (returningFromRagdoll)
        {
            timer += Time.deltaTime;
            float normalizedTime = timer / returnFromRagdollDuration;

            Transform[] bones = AnimationController.Instance.MovingBones;
            Transform[] animatingBones = AnimationController.Instance.AnimationBones;

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
                CapybaraController.Instance.SetMovementState(previousMovementState);
                AnimationController.Instance.Animator.enabled = true;

                spineBody.transform.parent = metaRig;
            }
        }
        else if (CapybaraController.Instance.CurrentMovementState == MovementState.Ragdoll)
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
        Transform[] bones = AnimationController.Instance.MovingBones;

        //if we are trying to ragdoll and it's already ragdolling, or stop ragdoll and it isn't ragdolling, do nothing
        if ((ragdoll && CapybaraController.Instance.CurrentMovementState == MovementState.Ragdoll) ||
           (!ragdoll && CapybaraController.Instance.CurrentMovementState != MovementState.Ragdoll))
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

            //set camera target
            CameraController.Instance.SetTarget(spineBody.transform, true);

            previousMovementState = CapybaraController.Instance.CurrentMovementState;

            //set spine velocity
            if (setVelocityFromMainBody)
            {
                spineBody.velocity = CapybaraController.Instance.MainBody.velocity;
            }

            AnimationController.Instance.Animator.enabled = false;

            CapybaraController.Instance.MainBody.velocity = Vector3.zero;
            CapybaraController.Instance.SetMovementState(MovementState.Ragdoll);

            ragdollTime = Time.time + minRagdollTime;
        }
        else
        {
            //set camera target
            CameraController.Instance.SetTarget(transform, false);

            CapybaraController.Instance.transform.position = spineBody.transform.position;
        }
    }
}
