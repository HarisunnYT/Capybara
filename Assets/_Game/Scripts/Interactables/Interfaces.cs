using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPullable
{
    void OnPulled();
    void OnDropped();

    GameObject GetObject();
}
