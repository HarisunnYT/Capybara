using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private float lifetime = 5;

    [SerializeField]
    protected LayerMask collisionLayers;

    public Rigidbody Rigidbody { get; private set; }

    private Collider collider;
    protected MeshRenderer meshRenderer;

    private float timeUntilColliderEnabled;
    private float timeUntilDestroy;

    private bool destroyed = false;

    protected virtual void Awake()
    {
        collider = GetComponent<Collider>();
        Rigidbody = GetComponent<Rigidbody>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
    }

    protected virtual void OnEnable()
    {
        timeUntilColliderEnabled = Time.time + 0.1f;
        timeUntilDestroy = Time.time + lifetime;

        Rigidbody.velocity = Vector3.zero;
        destroyed = false;

        meshRenderer.enabled = true;
        collider.enabled = false;
    }

    private void Update()
    {
        if (Time.time > timeUntilColliderEnabled)
        {
            collider.enabled = true;
        }

        if (!destroyed && Time.time > timeUntilDestroy)
        {
            OnDestroyed(transform.position, 0.2f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!destroyed && Util.CheckInsideLayer(collisionLayers, collision.gameObject.layer))
        {
            OnCollision(collision);
        }
    }

    protected virtual void OnCollision(Collision collision) { }

    protected virtual void OnDestroyed(Vector3 destroyPoint, float disableDelay) 
    {
        destroyed = true;

        meshRenderer.enabled = false;
        Rigidbody.velocity = Vector3.zero;

        Invoke("DisableWithDelay", disableDelay);
    }

    private void DisableWithDelay()
    {
        gameObject.SetActive(false);
    }
}
