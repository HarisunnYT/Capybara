using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUsingCurve : MonoBehaviour
{
    [SerializeField]
    private float moveDuration = 2;

    [SerializeField]
    private float normalizedTimeUntilCallback = 0.5f;

    [SerializeField]
    private AnimationCurve moveCurve;

    private System.Action callback;

    private bool doneCallback = false;

    IEnumerator MoveToHome(Vector3 targetPosition)
    {
        Vector3 startPoint = transform.position;

        float timeElapsed = 0;

        do
        {
            timeElapsed += Time.deltaTime;
            float normalizedTime = timeElapsed / moveDuration;

            Vector3 newPos = Vector3.Lerp(startPoint, targetPosition, normalizedTime);
            newPos.y = targetPosition.y + moveCurve.Evaluate(normalizedTime);

            transform.position = newPos;

            if (!doneCallback && normalizedTime >= normalizedTimeUntilCallback)
            {
                callback?.Invoke();
                doneCallback = true;
            }

            // Wait for one frame
            yield return null;
        }
        while (timeElapsed < moveDuration);
    }

    public void Move(Vector3 targetPos, System.Action completeCallback)
    {
        doneCallback = false;
        callback = completeCallback;

        StartCoroutine(MoveToHome(targetPos));
    }
}
