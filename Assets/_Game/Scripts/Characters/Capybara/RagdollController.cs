using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    struct BoneData
    {
        public Vector3 LocalPosition;
        public Quaternion LocalRotation;

        public BoneData (Vector3 localPosition, Quaternion localRotation)
        {
            LocalPosition = localPosition;
            LocalRotation = localRotation;
        }
    }

    public static RagdollController Instance;

    [SerializeField]
    private float returnFromRagdollDuration;

    [Space()]
    [SerializeField]
    private Rigidbody spineBody;

    [SerializeField]
    private Transform metaRig;

    [SerializeField]
    private Transform[] moveableBones;

    private Rigidbody[] bodies;

    //int == child index, quaternion == child rotation
    private List<BoneData> lastAnimationBoneData = new List<BoneData>();
    private List<BoneData> lastRagdollBoneData = new List<BoneData>();

    private float timer = 0;
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

            //loop through every bone and move to target
            for (int i = 0; i < moveableBones.Length; i++)
            {
                Quaternion fromRotation = lastRagdollBoneData[i].LocalRotation;
                Quaternion toRotation = lastAnimationBoneData[i].LocalRotation * transform.rotation;

                moveableBones[i].rotation = Quaternion.Lerp(fromRotation, toRotation, normalizedTime);

                if (moveableBones[i].parent != null)
                {
                    Vector3 fromPosition = lastRagdollBoneData[i].LocalPosition;
                    Vector3 toPosition = moveableBones[i].parent.TransformPoint(lastAnimationBoneData[i].LocalPosition);

                    moveableBones[i].position = Vector3.Lerp(fromPosition, toPosition, normalizedTime);
                }
            }

            if (normalizedTime > 0.99f)
            {
                returningFromRagdoll = false;
                CapybaraController.Instance.Animator.enabled = true;
            }
        }
        else if (CapybaraController.Instance.CurrentMovementState == MovementState.Ragdoll)
        {
            //check if the spine has stopped moving
            if (spineBody.velocity.magnitude < 0.1f)
            {
                SetRagdoll(false);
            }
        }
    }

    public void SetRagdoll(bool ragdoll)
    {
        //if we are trying to ragdoll and it's already ragdolling, or stop ragdoll and it isn't ragdolling, do nothing
        if ((ragdoll && CapybaraController.Instance.CurrentMovementState == MovementState.Ragdoll) ||
           (!ragdoll && CapybaraController.Instance.CurrentMovementState != MovementState.Ragdoll))
        {
            return;
        }

        //save all the bone rotations first
        if (ragdoll)
        {
            lastAnimationBoneData.Clear();
            for (int i = 0; i < moveableBones.Length; i++)
            {
                lastAnimationBoneData.Add(new BoneData(moveableBones[i].localPosition, moveableBones[i].rotation));
            }
        }
        else
        {
            lastRagdollBoneData.Clear();
            for (int i = 0; i < moveableBones.Length; i++)
            {
                lastRagdollBoneData.Add(new BoneData(moveableBones[i].position, moveableBones[i].rotation));
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
            spineBody.velocity = CapybaraController.Instance.MainBody.velocity;

            CapybaraController.Instance.Animator.enabled = false;
            CapybaraController.Instance.MainBody.velocity = Vector3.zero;
            CapybaraController.Instance.SetMovementState(MovementState.Ragdoll);

        }
        else
        {
            //set camera target
            CameraController.Instance.SetTarget(transform, false);
            CapybaraController.Instance.SetMovementState(previousMovementState);

            CapybaraController.Instance.transform.position = spineBody.transform.position;
            //spineBody.transform.parent = metaRig;
        }
    }
}
