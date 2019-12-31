using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapybaraHead : MonoBehaviour
{
    [SerializeField]
    private float moveDuration;

    private Transform movingToObject;
    private System.Action callback;
    private Quaternion originalLocalRotation;
    private Quaternion currentGlobalRotation;

    private float timer = 0;

    private void Start()
    {
        originalLocalRotation = transform.localRotation;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        float normalizedTime = timer / moveDuration;

        if (movingToObject != null)
        {
            transform.rotation = Quaternion.Slerp(currentGlobalRotation, Quaternion.LookRotation(movingToObject.transform.position - transform.position, Vector3.up), normalizedTime);

            if (normalizedTime > 0.99f)
            {
                movingToObject = null;
                callback?.Invoke();
                timer = 0;
            }
        }
        else if (normalizedTime < 1.0f)
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, originalLocalRotation, normalizedTime);
        }
    }

    public void MoveToObject(Transform obj, System.Action callback)
    {
        timer = 0;
        movingToObject = obj;
        currentGlobalRotation = transform.rotation;
        this.callback = callback;
    }
}
