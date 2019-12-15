using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    [SerializeField]
    private QuadrupedWalk quadrupedWalk;

    [SerializeField]
    private QuadrupedIdle quadrupedIdle;

    private QuadrupedAnim currentAnimation;

    private Vector2 inputAxis;

    private void Start()
    {
        Instance = this;
    }

    private void Update()
    {
        inputAxis = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (inputAxis == Vector2.zero)
        {
            if (currentAnimation == quadrupedWalk)
            {
                currentAnimation.StopAnimating();
            }
            currentAnimation = quadrupedIdle;
            currentAnimation.Animate();
        }
        else if (currentAnimation == quadrupedIdle)
        {
            quadrupedIdle.StopAnimating();
            currentAnimation = null;
        }
        else
        {
            currentAnimation = quadrupedWalk;
            quadrupedWalk.Walk(inputAxis);
        }
    }
}
