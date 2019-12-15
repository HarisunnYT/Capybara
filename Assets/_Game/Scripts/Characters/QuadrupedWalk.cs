using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadrupedWalk : QuadrupedAnim
{
    [SerializeField]
    private float moveDuration;

    [SerializeField]
    private float stepDistance = 4;

    [SerializeField]
    private AnimationCurve moveCurve;

    private bool moving = false;
    private int currentStepIndex = 0;

    private float elapsedTime = float.MaxValue;

    public void Walk(Vector2 inputVector)
    {
        float localStepDistance = stepDistance;

        if (inputVector == Vector2.zero)
        {
            moving = false;
            elapsedTime = float.MaxValue;
            currentStepIndex = 0;
        }
        else if (!moving)
        {
            //it's the first step, they don't need to walk as far
            localStepDistance /= 2;
            moving = true;
            elapsedTime = 0;
        }

        if (currentStepIndex > 1)
        {
            currentStepIndex = 0;
        }

        if (elapsedTime == 0)
        {
            Vector3 frontTargetPosition = frontLegs[currentStepIndex].transform.position + (frontLegs[currentStepIndex].transform.forward * localStepDistance);
            Vector3 backTargetPosition = backLegs[currentStepIndex].transform.position + (backLegs[currentStepIndex].transform.forward * localStepDistance);
            frontLegs[currentStepIndex].SetTarget(frontTargetPosition, moveDuration, false, moveCurve);
            backLegs[currentStepIndex].SetTarget(backTargetPosition, moveDuration, false, moveCurve);
        }

        elapsedTime += Time.deltaTime;
        float normalizedTime = elapsedTime / moveDuration;

        if (normalizedTime >= 1)
        {
            currentStepIndex++;
            elapsedTime = 0;
        }

        foreach(var leg in frontLegs)
        {
            leg.UpdateMovement(inputVector.y); //TODO rotation
        }

        foreach (var leg in backLegs)
        {
            leg.UpdateMovement(inputVector.y); //TODO rotation
        }
    }

    public override void StopAnimating()
    {
        base.StopAnimating();

        moving = false;
    }
}
