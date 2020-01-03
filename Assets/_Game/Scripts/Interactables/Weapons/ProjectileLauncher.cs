using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : TwoHandedWeapon
{
    [SerializeField]
    private ProjectileData projectileData;

    [SerializeField]
    private Vector3 spawnPosition;

    [SerializeField]
    private Axis shootDirection;

    [SerializeField]
    private float force;

    [SerializeField]
    private float knockBackForce;

    protected override void OnAttack()
    {
        LaunchProjectile();
    }

    private void LaunchProjectile()
    {
        GameObject projectile = ObjectPooler.GetPooledObject(projectileData.ProjectilePrefab);

        Vector3 worldPosition = transform.GetChild(0).TransformPoint(transform.GetChild(0).transform.localPosition + spawnPosition);

        projectile.transform.position = worldPosition;

        //shoot it 
        projectile.GetComponent<Rigidbody>().AddForce(Util.GetDirection(transform, shootDirection) * force * projectileData.ForceMultiplier, ForceMode.Impulse);

        //add knock back force to our player
        CapybaraController.Instance.AddKnockBackForce(-Util.GetDirection(transform, shootDirection), knockBackForce * projectileData.KnockBackForceMultiplier);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 worldPosition = transform.GetChild(0).TransformPoint(transform.GetChild(0).transform.localPosition + spawnPosition);
        Gizmos.DrawWireSphere(worldPosition, 0.1f);
    }
}
