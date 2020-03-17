using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Consumable : MonoBehaviour
{
    [SerializeField]
    private float pickUpDistance;

    [SerializeField]
    private float spawnPickUpDelay = 0.5f;

    private bool pickedUp = false;
    protected bool collected = false;

    private float lerpTarget = 0;
    private float spawnPickUpTimer;

    private Vector3 pickedUpPosition;

    private const float lerpDuration = 0.1f;

    private void OnEnable()
    {
        pickedUp = false;
        collected = false;
        lerpTarget = 0;

        spawnPickUpTimer = Time.time + spawnPickUpDelay;
    }

    private void Update()
    {
        if (collected)
            return;

        if (pickedUp)
        {
            lerpTarget += Time.deltaTime;
            float normalisedTime = lerpTarget / lerpDuration;

            transform.position = Vector3.Lerp(pickedUpPosition, GameManager.Instance.CapyController.Middle.position, normalisedTime);

            if (normalisedTime >= 1)
            {
                collected = true;
                OnPickedUp();
            }
        }
        else if (Time.time > spawnPickUpTimer && Vector3.Distance(transform.position, GameManager.Instance.CapyController.transform.position) < pickUpDistance)
        {
            pickedUpPosition = transform.position;
            pickedUp = true;
        }
    }

    protected virtual void OnPickedUp() { }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, pickUpDistance);
    }
}
