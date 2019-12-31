using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 2;

    [SerializeField]
    private float rotationSpeed = 2;

    private Rigidbody rigidbody;

    private Vector3 lastInputVector;

    private void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 inputVec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (inputVec != Vector3.zero)
        {
            inputVec = CameraController.Instance.transform.TransformDirection(inputVec);
            inputVec.y = 0;

            rigidbody.velocity = inputVec * moveSpeed;

            lastInputVector = inputVec;
        }

        Quaternion toRotation = Quaternion.LookRotation(lastInputVector, Vector3.up);
        Quaternion rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        rotation.eulerAngles = new Vector3(rotation.eulerAngles.x, rotation.eulerAngles.y, 0);
        transform.rotation = rotation;
    }
}
