using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadrupedIdle : QuadrupedAnim
{
    [Space()]
    [SerializeField]
    private float moveToIdleDuration = 0.5f;

    [SerializeField]
    private Vector3[] frontLegLocalPositions;

    [SerializeField]
    private Vector3[] backLegLocalPositions;

    public override void Animate()
    {
        if (!animating)
        {
            Vector3 averageFrontLegsPosition = (frontLegs[0].transform.position + frontLegs[1].transform.position) / 2;
            for (int i = 0; i < frontLegs.Length; i++)
            {
                frontLegs[i].SetTarget(averageFrontLegsPosition, moveToIdleDuration);
            }

            Vector3 averageBackLegsPosition = (backLegs[0].transform.position + backLegs[1].transform.position) / 2;
            for (int i = 0; i < backLegs.Length; i++)
            {
                backLegs[i].SetTarget(averageBackLegsPosition, moveToIdleDuration);
            }
        }

        base.Animate();
    }
}
