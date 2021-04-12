using System;
using System.Collections;
using System.Collections.Generic;
using Character;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Weapons
{
    public class WeaponHolder : MonoBehaviour
    {
        [SerializeField] private WeaponScriptable WeaponToSpawn;
        [SerializeField] private Transform WeaponSocket;

        private Transform GripLocation;

        // Components
        public PlayerController Controller => PlayerController;
        private PlayerController PlayerController;
        private Animator PlayerAnimator;

        private bool WasFiring = false;
        private bool FiringPressed = false;

        // Ref
        private Camera MainCamera;

        public WeaponComponent EquippedWeapon => WeaponComponent;
        private WeaponComponent WeaponComponent;

        // Animator Hashes
        private readonly int AimVerticalHash = Animator.StringToHash("AimVertical");
        private readonly int AimHorizontalHash = Animator.StringToHash("AimHorizontal");
        private readonly int IsFiringHash = Animator.StringToHash("IsFiring");

        private readonly int IsReloadingHash = Animator.StringToHash("IsReloading");
        private readonly int WeaponTypeHash = Animator.StringToHash("WeaponType");

        private void Awake()
        {
            PlayerController = GetComponent<PlayerController>();
            PlayerAnimator = GetComponent<Animator>();

            MainCamera = Camera.main;
        }

        // Start is called before the first frame update
        void Start()
        {
            //if (WeaponToSpawn) EquipWeapon(WeaponToSpawn);
        }

        public void OnLook(InputValue delta)
        {
            Vector3 independentMousePosition =
                MainCamera.ScreenToViewportPoint(PlayerController.CrosshairComponent.CurrentMousePosition);

            PlayerAnimator.SetFloat(AimVerticalHash, independentMousePosition.y);
            PlayerAnimator.SetFloat(AimHorizontalHash, independentMousePosition.x);
        }

        public void OnFire(InputValue button)
        {
            FiringPressed = button.isPressed;
            if (FiringPressed)
                StartFiring();
            else
                StopFiring();
        }

        private void StartFiring()
        {
            if (EquippedWeapon == null) return;

            if (EquippedWeapon.WeaponStats.TotalBulletsAvailable <= 0
                && EquippedWeapon.WeaponStats.BulletsInClip <= 0) return;

            PlayerController.isFiring = true;
            PlayerAnimator.SetBool(IsFiringHash, PlayerController.isFiring);
            EquippedWeapon.StartFiring();
        }

        private void StopFiring()
        {
            if (EquippedWeapon == null) return;

            PlayerController.isFiring = false;
            PlayerAnimator.SetBool(IsFiringHash, PlayerController.isFiring);
            EquippedWeapon.StopFiring();
        }

        public void OnReload(InputValue button)
        {
            if (EquippedWeapon == null) return;

            StartReloading();
        }

        public void StartReloading()
        {
            if (EquippedWeapon.WeaponStats.TotalBulletsAvailable <= 0 && PlayerController.isFiring)
            {
                StopFiring();
                return;
            }

            PlayerController.isReloading = true;
            PlayerAnimator.SetBool(IsReloadingHash, PlayerController.isReloading);
            EquippedWeapon.StartReloading();

            InvokeRepeating(nameof(StopReloading), 0, 0.1f);
        }

        public void StopReloading()
        {
            if (PlayerAnimator.GetBool(IsReloadingHash)) return;

            PlayerController.isReloading = false;
            EquippedWeapon.StopReloading();
            CancelInvoke(nameof(StopReloading));

            if (WasFiring || FiringPressed) return;

            StartFiring();
            WasFiring = false;
        }

        private void OnAnimatorIK(int layerIndex)
        {
            if (EquippedWeapon == null) return;

            PlayerAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, 1.0f);
            PlayerAnimator.SetIKPosition(AvatarIKGoal.LeftHand, GripLocation.position);
        }

        public void EquipWeapon(WeaponScriptable weaponScriptable)
        {
            GameObject spawnedWeapon = Instantiate(weaponScriptable.ItemPrefab, WeaponSocket.position, WeaponSocket.rotation);

            if (!spawnedWeapon) return;

            spawnedWeapon.transform.parent = WeaponSocket;
            WeaponComponent = spawnedWeapon.GetComponent<WeaponComponent>();

            GripLocation = EquippedWeapon.HandPosition;

            EquippedWeapon.Initialize(this, weaponScriptable);
            PlayerAnimator.SetInteger(WeaponTypeHash, (int)EquippedWeapon.WeaponStats.WeaponType);

            PlayerEvents.Invoke_OnWeaponEquipped(EquippedWeapon);
        }

        public void UnequipWeapon()
        {
            Destroy(EquippedWeapon.gameObject);
            WeaponComponent = null;
        }

    }
}