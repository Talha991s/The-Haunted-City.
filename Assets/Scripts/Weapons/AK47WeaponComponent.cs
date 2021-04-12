using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    public class AK47WeaponComponent : WeaponComponent
    {
        private Camera ViewCamera;

        private RaycastHit HitLocation;

        private void Awake()
        {
            ViewCamera = Camera.main;
        }

        protected override void FireWeapon()
        {
            Ray screenRay = ViewCamera.ScreenPointToRay(new Vector3(Crosshair.CurrentMousePosition.x,
                Crosshair.CurrentMousePosition.y, 0));

            if (WeaponStats.BulletsInClip > 0 && !Reloading && !WeaponHolder.Controller.isJumping)
            {
                base.FireWeapon();

                if (!Physics.Raycast(screenRay, out RaycastHit hit, WeaponStats.FireDistance, WeaponStats.WeaponHitLayer)) return;

                Vector3 RayDirection = HitLocation.point - ViewCamera.transform.position;

                Debug.DrawRay(ViewCamera.transform.position, RayDirection * WeaponStats.FireDistance, Color.red);

                HitLocation = hit;

                DamageTarget(hit);
            }
            else if (WeaponStats.BulletsInClip <= 0)
            {
                WeaponHolder.StartReloading();
            }
        }

        private void DamageTarget(RaycastHit hit)
        {
            IDamageable damageable = hit.collider.GetComponent<IDamageable>();

            damageable?.TakeDamage(WeaponStats.Damage);
        }

        private void OnDrawGizmos()
        {
            if (HitLocation.transform)
            {
                Gizmos.DrawSphere(HitLocation.point, 0.2f);
            }
        }
    }
}