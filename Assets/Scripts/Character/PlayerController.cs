using System;
using System.Linq;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Weapons;

namespace Character
{
    public class PlayerController : MonoBehaviour, IPausable, ISaveable
    {
        public CrosshairScript CrosshairComponent => CrosshairScript;
        [SerializeField] private CrosshairScript CrosshairScript;

        private GameUIController GameUIController;

        public HealthComponent Health => HealthComponent;
        private HealthComponent HealthComponent;

        public InventoryComponent Inventory => InventoryComponent; 
        private InventoryComponent InventoryComponent;

        public WeaponHolder EquippedWeapon => WeaponHolderComponent;
        private WeaponHolder WeaponHolderComponent;

        private PlayerInput PlayerInput;

        [SerializeField] ConsumableScriptable Consume;

        public bool isFiring;
        public bool isReloading;
        public bool isJumping;
        public bool isRunning;
        public bool inInventory;

        private void Awake()
        {
            if (GameUIController == null) GameUIController = FindObjectOfType<GameUIController>();
            if (PlayerInput == null) PlayerInput = GetComponent<PlayerInput>();
            if (WeaponHolderComponent == null) WeaponHolderComponent = GetComponent<WeaponHolder>();
            if (HealthComponent == null) HealthComponent = GetComponent<HealthComponent>();
            if (InventoryComponent == null) InventoryComponent = GetComponent<InventoryComponent>();
        }

        private void Start()
        {
            //Health.TakeDamage(50);
            //Consume.UseItem(this);
        }

        public void OnPauseGame()
        {
            PauseManager.Instance.PauseGame();
        }

        public void OnUnpauseGame()
        {
            PauseManager.Instance.UnpauseGame();
        }

        public void PauseGame()
        {
            GameUIController.EnablePauseMenu();
            if (PlayerInput)
            {
                PlayerInput.SwitchCurrentActionMap("PauseActionMap");
            }
        }

        public void UnpauseGame()
        {
            GameUIController.EnableGameMenu();
            if (PlayerInput)
            {
                PlayerInput.SwitchCurrentActionMap("ThirdPerson");
            }
        }

        public void OnInventory(InputValue button)
        {
            if (inInventory)
            {
                inInventory = false;
                OpenInventory(false);
            }
            else
            {
                inInventory = true;
                OpenInventory(true);
            }
        }

        private void OpenInventory(bool open)
        {
            if (open)
            {
                PauseManager.Instance.PauseGame();
                GameUIController.EnableInventoryMenu();
            }
            else
            {
                PauseManager.Instance.UnpauseGame();
                GameUIController.EnableGameMenu();
            }
        }

        public void OnSave(InputValue button)
        {
            SaveSystem.Instance.SaveGame();
        }

        public void OnLoad(InputValue button)
        {
            SaveSystem.Instance.LoadGame();
        }

        public SaveDataBase SaveData()
        {
            WeaponSaveData EquippedWeaponData = new WeaponSaveData();

            if (WeaponHolderComponent.EquippedWeapon)
            {
                EquippedWeaponData = new WeaponSaveData(WeaponHolderComponent.EquippedWeapon.WeaponStats);
            }

            PlayerSaveData saveData = new PlayerSaveData
            {
                Name = gameObject.name,
                Position = transform.position,
                Rotation = transform.rotation,
                CurrentHealth = HealthComponent.Health,
                EquippedWeaponData = EquippedWeaponData
            };

            var itemSaveList = Inventory.GetItemList().Select(item => new ItemSaveData(item)).ToList();
            saveData.ItemList = itemSaveList;

            return saveData;

            //Transform, Health, Name, Item List, Weapon Stats

        }

        public void LoadData(SaveDataBase saveData)
        {
            PlayerSaveData playerData = (PlayerSaveData)saveData;
            if (playerData == null) return;

            Transform playerTransform = transform;
            playerTransform.position = playerData.Position;
            playerTransform.rotation = playerData.Rotation;

            Health.SetCurrentHealth(playerData.CurrentHealth);

            foreach (ItemSaveData itemSaveData in playerData.ItemList)
            {
                ItemScriptables item = InventoryReferencer.Instance.GetItemReference(itemSaveData.Name);
                Inventory.AddItem(item, itemSaveData.Amount);
            }

            WeaponScriptable weapon = (WeaponScriptable)Inventory.FindItem(playerData.EquippedWeaponData.Name);

            if (!weapon) return;

            weapon.WeaponStats = playerData.EquippedWeaponData.WeaponStats;
            WeaponHolderComponent.EquipWeapon(weapon);
        }
    }
}
