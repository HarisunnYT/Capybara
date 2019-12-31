using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPullable
{
    void OnPulled();
    void OnDropped();

    GameObject GetObject();
}

public interface IPickupable
{
    void OnPickedUp();
    void OnDropped();

    Vector3 GetOrientation();

    GameObject GetObject();

    Rigidbody GetRigidbody();

    Transform GetBone();
}
