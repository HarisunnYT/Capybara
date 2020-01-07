using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public CharacterController CapyController;

    public bool IsPlayer(CharacterController controller)
    {
        return controller == CapyController;
    }
}
