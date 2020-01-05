using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LuxLWRPEssentials.Demo {
    public class ToggleRimSelection : MonoBehaviour
    {
        public Color ActiveRimColor = new Color(0.137f, 0.8f, 0.988f, 0.364f);
    	
        [Header("Testing")]
        public bool Selected = false;

    	MeshRenderer rend;
    	bool Changed = false;
    	int RimColorPID;

    	MaterialPropertyBlock block;
    	
        void OnEnable()
        {
        	rend = GetComponent<MeshRenderer>();
            RimColorPID = Shader.PropertyToID("_RimColor");

            block = new MaterialPropertyBlock();
            block.Clear();
            block.SetColor(RimColorPID, ActiveRimColor);
        //  Make sure that the rim color is set to black on all instances of the material
            rend.sharedMaterial.SetColor(RimColorPID, Color.black);
        }

        void Update()
        {
            if(Selected != Changed) {
            	Changed = Selected;
            	if (Selected) {
            		rend.SetPropertyBlock(block);	
            	}
            	else {
            		rend.SetPropertyBlock(null);
            	}
            }
        }
    }
}