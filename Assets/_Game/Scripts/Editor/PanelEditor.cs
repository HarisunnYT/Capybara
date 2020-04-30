using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Panel), true)]
public class PanelEditor : Editor
{
    public override void OnInspectorGUI()
    {
        if (Application.isPlaying)
        {
            Panel panel = (Panel)target;
            if (GUILayout.Button("Show"))
            {
                panel.ShowPanel();
            }
            if (GUILayout.Button("Close"))
            {
                panel.Close();
            }
        }

        base.OnInspectorGUI();
    }
}
