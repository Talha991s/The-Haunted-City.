using Weapons;

namespace Character
{
    [System.Serializable]
    public class WeaponSaveData : SaveDataBase
    {
        public WeaponStats WeaponStats;

        public WeaponSaveData() 
        {
            Name = "None";
        }
       
        public WeaponSaveData(WeaponStats weaponStats)
        {
            Name = weaponStats.Name;
            WeaponStats = weaponStats;
        }

        public void SetWeaponSaveData(WeaponStats weaponStats)
        {
            Name = weaponStats.Name;
            WeaponStats = weaponStats;
        }
    }
}
