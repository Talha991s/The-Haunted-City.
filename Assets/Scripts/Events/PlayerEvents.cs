using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;

public class PlayerEvents
{
    public delegate void WeaponEquipped(WeaponComponent weapon);

    public static event WeaponEquipped OnWeaponEquipped;

    public static void Invoke_OnWeaponEquipped(WeaponComponent weapon)
    {
        OnWeaponEquipped?.Invoke(weapon);
    }

    public delegate void PlayerHealthSet(HealthComponent healthComponent);

    public static event PlayerHealthSet OnPlayerHealthSet;

    public static void Invoke_OnPlayerHealthSet(HealthComponent healthComponent)
    {
        OnPlayerHealthSet?.Invoke(healthComponent);
    }
}
