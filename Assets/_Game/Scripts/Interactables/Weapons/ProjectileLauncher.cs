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
        Projectile projectile = ObjectPooler.GetPooledObject(projectileData.ProjectilePrefab).GetComponent<Projectile>();

        Vector3 worldPosition = transform.GetChild(0).TransformPoint(transform.GetChild(0).transform.localPosition + spawnPosition);

        projectile.transform.position = worldPosition;
        projectile.transform.rotation = Quaternion.LookRotation(Util.GetDirection(transform, shootDirection), Vector3.up);

        //shoot it 
        projectile.Rigidbody.AddForce(projectile.transform.forward * force * projectileData.ForceMultiplier, ForceMode.Impulse);

        //add knock back force to our player
        CapybaraController.Instance.AddKnockBackForce(-Util.GetDirection(transform, shootDirection), knockBackForce * projectileData.KnockBackForceMultiplier);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 worldPosition = transform.GetChild(0).TransformPoint(transform.GetChild(0).transform.localPosition + spawnPosition);
        Gizmos.DrawWireSphere(worldPosition, 0.1f);
    }
}
