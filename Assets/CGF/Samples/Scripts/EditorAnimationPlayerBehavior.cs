using System.Collections;
using UnityEngine;

public class EditorAnimationPlayerBehavior : MonoBehaviour {
	
	public bool enable = true;
	
	public float speed = 1;
     	 
    void OnDrawGizmos()
	{
		 
		Animator myAnimator = gameObject.GetComponent<Animator>();
		
		if (enable) {
			
			myAnimator.Play("Rotation");

			myAnimator.Update(Time.deltaTime * speed);
		
		}
		
	}
	 
}