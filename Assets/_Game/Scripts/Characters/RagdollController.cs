using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [SerializeField]
    private float returnFromRagdollDuration;

    private Rigidbody[] bodies;
    private Transform[] children;

    //int == child index, quaternion == child rotation
    private List<BoneData> lastAnimationBoneData = new List<BoneData>();
    private List<BoneData> lastRagdollBoneData = new List<BoneData>();

    private float timer = 0;
    private bool returningFromRagdoll = false;

    private void Start()
    {
        List<Rigidbody> bodies = GetComponentsInChildren<Rigidbody>().ToList();
        children = GetComponentsInChildren<Transform>();

        //get all the bodies, then remove the one on this transform
        for (int i = 0; i < bodies.Count; i++)
        {
            if (bodies[i] == CapybaraController.Instance.MainBody)
            {
                bodies.RemoveAt(i);
                break;
            }
        }

        this.bodies = bodies.ToArray();
    }

    private void Update()
    {
        if (returningFromRagdoll)
        {
            timer += Time.deltaTime;
            float normalizedTime = timer / returnFromRagdollDuration;

            //loop through every bone and move to target
            for (int i = 0; i < children.Length; i++)
            {
                Quaternion fromRotation = lastRagdollBoneData[i].LocalRotation;
                Quaternion toRotation = lastAnimationBoneData[i].LocalRotation;

                Vector3 fromPosition = lastRagdollBoneData[i].LocalPosition;
                Vector3 toPosition = lastAnimationBoneData[i].LocalPosition;

                children[i].localRotation = Quaternion.Lerp(fromRotation, toRotation, normalizedTime);
                children[i].localPosition = Vector3.Lerp(fromPosition, toPosition, normalizedTime);
            }

            if (normalizedTime > 0.99f)
            {
                returningFromRagdoll = false;
                CapybaraController.Instance.Animator.enabled = true;
            }
        }
    }

    private void SetRagdoll(bool ragdoll)
    {
        //save all the bone rotations first
        if (ragdoll)
        {
            lastAnimationBoneData.Clear();
            for (int i = 0; i < children.Length; i++)
            {
                lastAnimationBoneData.Add(new BoneData(children[i].localPosition, children[i].localRotation));
            }
        }
        else
        {
            lastRagdollBoneData.Clear();
            for (int i = 0; i < children.Length; i++)
            {
                lastRagdollBoneData.Add(new BoneData(children[i].localPosition, children[i].localRotation));
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
            CapybaraController.Instance.Animator.enabled = false;
        }
    }
}
