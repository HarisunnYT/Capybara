using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    [SerializeField]
    private float scrollSpeed;

    [SerializeField]
    private float loopValue;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float newPos = Mathf.Repeat(scrollSpeed * Time.time, loopValue);
        transform.localPosition = startPos - Vector3.left * newPos;
    }
}
