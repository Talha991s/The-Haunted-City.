using Character;
using UnityEngine;
using Weapons;

[CreateAssetMenu(fileName = "Item", menuName = "Items/Weapon", order = 1)]
public class WeaponScriptable : EquippableScriptable
{
    public WeaponStats WeaponStats;

    public override void UseItem(PlayerController controller)
    {
        base.UseItem(controller);
        if (Equipped)
        {
            controller.EquippedWeapon.EquipWeapon(this);
        }
        else
        {
            controller.EquippedWeapon.UnequipWeapon();
        }
    }
}