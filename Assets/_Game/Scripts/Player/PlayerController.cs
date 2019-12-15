using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField]
    private LegNode[] frontLegs;

    [SerializeField]
    private LegNode[] backLegs;

    private int currentLegIndex = 0;

    private Vector2 inputAxis;
    private bool moving = false;

    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        inputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (inputAxis.y > 0 && !moving)
        {
            moving = true;
            MoveLeg();
        }

        moving = inputAxis.y > 0;
    }

    private void MoveLeg()
    {
        if (!moving)
        {
            return;
        }

        if (currentLegIndex > 1)
        {
            currentLegIndex = 0;
        }

        float distanceMultiplier = 4;

        frontLegs[currentLegIndex].Move(frontLegs[currentLegIndex].transform.position + (frontLegs[currentLegIndex].transform.forward * distanceMultiplier), () =>
        {
            MoveLeg();
        });

        backLegs[currentLegIndex].Move(backLegs[currentLegIndex].transform.position + (backLegs[currentLegIndex].transform.forward * distanceMultiplier), () =>
        {
        });

        currentLegIndex++;
    }
}
