using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Weapons
{
    public enum WeaponType
    {
        None, 
        MachineGun,
        Pistol
    }

    [Serializable]
    public struct WeaponStats
    {
        public WeaponType WeaponType;
        public string Name;
        public float Damage;
        public float BulletsInClip;
        public int ClipSize;
        public int TotalBulletsAvailable;

        public float FireStartDelay;
        public float FireRate;
        public float FireDistance;
        public bool Repeating;

        public LayerMask WeaponHitLayer;
    }

    public class WeaponComponent : MonoBehaviour
    {
        public Transform HandPosition => GripIKLocation;
        [SerializeField] Transform GripIKLocation;

        public bool Firing { get; private set; } = false;
        public bool Reloading { get; private set; } = false;

        public WeaponStats WeaponStats;

        protected WeaponHolder WeaponHolder;
        protected CrosshairScript Crosshair;

        public void Initialize(WeaponHolder weaponHolder, WeaponScriptable weapon)
        {
            WeaponHolder = weaponHolder;
            Crosshair = weaponHolder.Controller.CrosshairComponent;

            WeaponStats = weapon.WeaponStats;
        }

        public virtual void StartFiring()
        {
            Firing = true;
            if (WeaponStats.Repeating)
            {
                InvokeRepeating(nameof(FireWeapon), WeaponStats.FireStartDelay, WeaponStats.FireRate);
            }
            else
            {
                FireWeapon();
            }
        }

        public virtual void StopFiring()
        {
            Firing = false;
            CancelInvoke(nameof(FireWeapon));
        }

        protected virtual void FireWeapon()
        {
            WeaponStats.BulletsInClip--;
        }

        public virtual void StartReloading()
        {
            Reloading = true;
            ReloadWeapon();
        }

        public virtual void StopReloading()
        {
            Reloading = false;
        }

        protected virtual void ReloadWeapon()
        {
            int bulletToReload = WeaponStats.ClipSize - WeaponStats.TotalBulletsAvailable;
            if (bulletToReload < 0)
            {
                WeaponStats.BulletsInClip = WeaponStats.ClipSize;
                WeaponStats.TotalBulletsAvailable -= WeaponStats.ClipSize;
            }
            else
            {
                WeaponStats.BulletsInClip = WeaponStats.TotalBulletsAvailable;
                WeaponStats.TotalBulletsAvailable = 0;
            }
        }
    }
}
