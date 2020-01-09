using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : Controller
{
    [SerializeField]
    private CharacterType characterType;
    public CharacterType CharacterType { get { return characterType; } }

    public Transform Parent { get; private set; }

    private void Start()
    {
        Parent = transform.parent;
    }

    public void ResetParent()
    {
        transform.parent = Parent;
    }
}
