using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : CharacterController
{
    public AIMovementController AIMovementController { get { return MovementController as AIMovementController; } }
}
