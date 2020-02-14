using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterType
{
    Capybara,
    Zookeeper
}

public class GameManager : Singleton<GameManager>
{
    public CharacterController CapyController;

    [SerializeField]
    private bool debugLockMouse = false;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;

        EnableCursor(false);

#if UNITY_EDITOR
        if (debugLockMouse)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
#endif
    }

    public void EnableCursor(bool enable)
    {
        Cursor.visible = enable;
    }

    public bool IsPlayer(CharacterController controller)
    {
        return controller == CapyController;
    }
}
