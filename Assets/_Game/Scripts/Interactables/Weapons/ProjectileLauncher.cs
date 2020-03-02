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

    public override void PickUpItem(Transform parent, BodyPart currentBodyPart, CharacterController controller)
    {
        base.PickUpItem(parent, currentBodyPart, controller);

        collider.enabled = false;
    }

    public override void DropItem()
    {
        base.DropItem();

        collider.enabled = true;
    }

    protected override void OnAttack()
    {
        base.OnAttack();

        LaunchProjectile();
    }

    private void LaunchProjectile()
    {
        //get raycast hit point
        Vector3 rayStartPoint = CameraController.Instance.Cam.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, CameraController.Instance.Cam.nearClipPlane));

        Projectile projectile = ObjectPooler.GetPooledObject(projectileData.ProjectilePrefab).GetComponent<Projectile>();

        Vector3 worldPosition = transform.GetChild(0).TransformPoint(transform.GetChild(0).transform.localPosition + spawnPosition);

        projectile.transform.position = worldPosition;

        if (CurrentController == GameManager.Instance.CapyController && CurrentController.AimController.IsAiming)
        {
            projectile.transform.LookAt(rayStartPoint + CameraController.Instance.Cam.transform.forward * 5);
        }
        else
        {
            projectile.transform.rotation = Quaternion.LookRotation(Util.GetDirection(transform, shootDirection), Vector3.up);
        }

        //shoot it 
        projectile.Rigidbody.AddForce(projectile.transform.forward * force * projectileData.ForceMultiplier, ForceMode.Impulse);

        projectile.Configure(projectileData);

        //add knock back force to our player
        CurrentController.MovementController.AddKnockBackForce(-Util.GetDirection(transform, shootDirection), knockBackForce * projectileData.KnockBackForceMultiplier);
    }

    private void OnDrawGizmosSelected()
    {
        Vector3 worldPosition = transform.GetChild(0).TransformPoint(transform.GetChild(0).transform.localPosition + spawnPosition);
        Gizmos.DrawWireSphere(worldPosition, 0.1f);
    }
}
