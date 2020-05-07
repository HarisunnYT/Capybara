using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Leg : MonoBehaviour
{
    [SerializeField]
    private Transform body;

    [SerializeField]
    private Transform legBodyTarget;

    [SerializeField]
    private Transform legTarget;

    [SerializeField]
    private LayerMask layerMask;

    [Space()]
    [SerializeField]
    private float maxDistance = 10;

    [SerializeField]
    private float moveDuration = 0.5f;

    [SerializeField]
    private float yMovement = 2;

    private RaycastHit hit;

    private void Update()
    {
        if (Physics.Raycast(new Vector3(legBodyTarget.position.x, body.position.y, legBodyTarget.position.z), Vector3.down, out hit, 100, layerMask))
        {
            legBodyTarget.position = new Vector3(legBodyTarget.position.x, hit.point.y, legBodyTarget.position.z);
        }

        float distance = Vector3.Distance(new Vector3(legTarget.position.x, 0, legTarget.position.z), new Vector3(legBodyTarget.position.x, 0, legBodyTarget.position.z));
        if (distance > maxDistance)
        {
            legTarget.DOMoveX(legBodyTarget.position.x, moveDuration);
            legTarget.DOMoveZ(legBodyTarget.position.z, moveDuration);
            legTarget.DOMoveY(legBodyTarget.position.y + yMovement, moveDuration / 2).OnComplete(() =>
            {
                legTarget.DOMoveY(legBodyTarget.position.y, moveDuration / 2);
            });
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(legTarget.position, legBodyTarget.position);
    }
}
