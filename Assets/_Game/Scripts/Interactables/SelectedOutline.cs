using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SelectedOutline : MonoBehaviour
{
    private Material[] materials;

    private bool selected = false;

    private void Awake()
    {
        List<Material> mats = new List<Material>();
        foreach (var renderer in GetComponentsInChildren<MeshRenderer>())
        {
            foreach(var mat in renderer.sharedMaterials)
            {
                mats.Add(mat);
            }
        }

        materials = mats.ToArray();

        SetSelected(false);
    }

    private void Update()
    {
        bool inRange = Vector3.Distance(transform.position, GameManager.Instance.CapyController.transform.position) < GameManager.Instance.CapyController.InteractionController.InteractionRadius;
        if (inRange && !selected)
        {
            SetSelected(true);
        }
        else if (!inRange && selected)
        {
            SetSelected(false);
        }
    }

    private void SetSelected(bool selected)
    {
        foreach (var mat in materials)
        {
            mat.SetFloat("_Rim", selected ? 1 : 0);
        }

        this.selected = selected;
    }
}
