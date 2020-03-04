using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public enum RoomType { standard, treasure, boss };
    public RoomType roomType;

    [SerializeField]
    private GameObject floorGuide;

    [SerializeField]
    private Transform northExit, southExit, eastExit, westExit;

    void Start()
    {
        Destroy(floorGuide);
    }
}
