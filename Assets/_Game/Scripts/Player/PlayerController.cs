using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [System.Serializable]
    struct Legs
    {
        public Feet Feet;
        public AddForce AddForce;
    }

    [SerializeField]
    private float force = 0;

    [SerializeField]
    private Legs[] legs = null;

    private Animator animator;
    private new Rigidbody rigidbody;

    private CharacterJoint joint;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < legs.Length; i++)
            {
                legs[i].Feet.Lock(false);
                legs[i].AddForce.AddDirectionalForce(appliedForce: force);
            }
        }
    }
}
