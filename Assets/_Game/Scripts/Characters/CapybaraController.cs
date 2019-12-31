using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemSlotType
{
    Head,
    Neck,
    Torso,
    Mouth,
    TwoHand,
    LeftHand,
    RightHand,
    LeftFoot,
    RightFoot,
    Bum
}

public class CapybaraController : MonoBehaviour
{
    public static CapybaraController Instance;

    [SerializeField]
    private float movementSpeed = 10;

    [SerializeField]
    private float rotationSpeed = 5;

    public Rigidbody MainBody { get; private set; }
    public Animator Animator { get; private set; }

    public BodyPart[] BodyParts { get; private set; }
    public Collider[] Colliders { get; private set; }

    private Vector3 lastInputVec;

    private void Awake()
    {
        Instance = this;

        MainBody = GetComponent<Rigidbody>();
        Animator = GetComponent<Animator>();

        BodyParts = GetComponentsInChildren<BodyPart>();
        Colliders = GetComponentsInChildren<Collider>();
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 inputVec = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        if (inputVec != Vector3.zero)
        {
            inputVec = CameraController.Instance.transform.TransformDirection(inputVec);
            inputVec *= movementSpeed;

            inputVec.y = MainBody.velocity.y;

            MainBody.velocity = inputVec;
            lastInputVec = inputVec;
        }

        Quaternion toRotation = Quaternion.LookRotation(lastInputVec, Vector3.up);
        Quaternion rotation = Quaternion.Lerp(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

        rotation.eulerAngles = new Vector3(0, rotation.eulerAngles.y, 0);
        transform.rotation = rotation;
    }
}
