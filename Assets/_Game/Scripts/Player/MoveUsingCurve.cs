using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUsingCurve : MonoBehaviour
{
    private float moveDuration = 2;
    private Vector3 targetPosition;
    private Vector3 startPosition;

    private AnimationCurve moveCurve;

    private float timeElapsed = float.MaxValue;
    private bool local = false;

    public void UpdateMovement(float multiplier)
    {
        if (timeElapsed < moveDuration && multiplier != 0)
        {
            timeElapsed += Time.deltaTime;
            float normalizedTime = timeElapsed / moveDuration;

            Vector3 newPos = Vector3.Lerp(startPosition, targetPosition, normalizedTime);
            //newPos.y = targetPosition.y + moveCurve.Evaluate(normalizedTime);

            if (local)
            {
                transform.localPosition = newPos;
            }
            else
            {
                transform.position = newPos;
            }
        }
    }

    public void SetTarget(Vector3 targetPosition, float moveDuration, bool local = false, AnimationCurve moveCurve = default)
    {
        this.moveDuration = moveDuration;
        this.moveCurve = moveCurve;
        this.targetPosition = targetPosition;
        startPosition = local ? transform.localPosition : transform.position;
        this.local = local;

        timeElapsed = 0;
    }
}
