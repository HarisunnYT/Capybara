using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RagdollController), true)]
public class RagdollControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (Application.isPlaying)
        {
            RagdollController controller = (RagdollController)target;

            if (GUILayout.Button("Enable Ragdoll"))
            {
                controller.SetRagdoll(true);
            }
        }
    }
}
