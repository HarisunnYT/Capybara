using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct PosAndRot
{
    public Vector3 Position;
    public Quaternion Rotation;
}

public class AnimationKeyFrame : MonoBehaviour
{
    [System.Serializable]
    public struct FrameSet
    {
        public Frame[] Frames;
    }

    [System.Serializable]
    public struct Frame
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public Transform Bone;

        [Space()]
        public AnimationCurve xCurve;
        public AnimationCurve yCurve;
        public AnimationCurve zCurve;
    }

    public AnimationState AnimState;
    public float Duration;

    [SerializeField]
    private CharacterMove characterMove;
    public CharacterMove CharacterMove { get { return characterMove; } }

    [SerializeField]
    private FrameSet[] animationFrames;
    public FrameSet[] AnimationFrames { get { return animationFrames; } }

    public PosAndRot GetBonePosAndRot(int frameIndex, int boneIndex, float normalizedTime)
    {
        Frame frame = animationFrames[frameIndex].Frames[boneIndex];
        Vector3 position = frame.Position;
        Quaternion rotation = frame.Rotation;

        position.x += frame.xCurve.Evaluate(normalizedTime);
        position.y += frame.yCurve.Evaluate(normalizedTime);
        position.y += frame.zCurve.Evaluate(normalizedTime);

        return new PosAndRot() { Position = position, Rotation = rotation };
    }
}
