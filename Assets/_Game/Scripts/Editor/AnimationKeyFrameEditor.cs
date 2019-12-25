using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(AnimationKeyFrame))]
public class AnimationKeyFrameEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        AnimationKeyFrame anim = target as AnimationKeyFrame;
        if (anim.CharacterMove == null)
        {
            anim.CharacterMove = anim.transform.GetComponent<CharacterMove>();
        }

        GUILayout.BeginVertical();
        if (anim.AnimationFrames != null)
        {
            for (int i = 0; i < anim.AnimationFrames.Length; i++)
            {
                GUILayout.BeginHorizontal();
                if (GUILayout.Button(string.Format("Set {0} Bones", i)))
                {
                    anim.AnimationFrames[i].Frames = new AnimationKeyFrame.Frame[anim.CharacterMove.Bones.Length];
                    for (int b = 0; b < anim.CharacterMove.Bones.Length; b++)
                    {
                        anim.AnimationFrames[i].Frames[b].Bone = anim.CharacterMove.Bones[b];
                        anim.AnimationFrames[i].Frames[b].Position = anim.CharacterMove.Bones[b].localPosition;
                        anim.AnimationFrames[i].Frames[b].Rotation = anim.CharacterMove.Bones[b].localRotation;
                    }
                }
                if (GUILayout.Button(string.Format("View {0} Bones", i)))
                {
                    for (int b = 0; b < anim.CharacterMove.Bones.Length; b++)
                    {
                        anim.CharacterMove.Bones[b] = anim.AnimationFrames[i].Frames[b].Bone;
                        anim.CharacterMove.Bones[b].localPosition = anim.AnimationFrames[i].Frames[b].Position;
                        anim.CharacterMove.Bones[b].localRotation = anim.AnimationFrames[i].Frames[b].Rotation;
                    }
                }
                GUILayout.EndHorizontal();
            }
        }
        GUILayout.EndVertical();
    }
}
