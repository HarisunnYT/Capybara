using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum LegType
{
    Front,
    Back
}

public class LegNode : MoveUsingCurve
{
    [SerializeField]
    private LegNode otherLeg;
    public LegNode OtherLeg { get { return otherLeg; } }

    [SerializeField]
    private LegType legType;
    public LegType LegType { get { return legType; } }
}
