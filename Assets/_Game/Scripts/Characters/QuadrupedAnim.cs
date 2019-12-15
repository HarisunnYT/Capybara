using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadrupedAnim : MonoBehaviour
{
    [SerializeField]
    protected LegNode[] frontLegs;

    [SerializeField]
    protected LegNode[] backLegs;

    protected bool animating = false;

    public virtual void Animate() 
    {
        animating = true;

        foreach (var leg in frontLegs)
        {
            leg.UpdateMovement(1);
        }

        foreach (var leg in backLegs)
        {
            leg.UpdateMovement(1);
        }
    }

    public virtual void StopAnimating() 
    {
        animating = false;
    }
}
