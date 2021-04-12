using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    [System.Serializable]
    public class PlayerSaveData : SaveDataBase
    {
        public float CurrentHealth;
        public Vector3 Position;
        public Quaternion Rotation;
        public WeaponSaveData EquippedWeaponData;
        public List<ItemSaveData> ItemList;
    }
}
