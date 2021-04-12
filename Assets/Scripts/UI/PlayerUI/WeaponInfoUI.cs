using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using TMPro;

public class WeaponInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI CurrentClipText;
    [SerializeField] private TextMeshProUGUI WeaponNameText;
    [SerializeField] private TextMeshProUGUI TotalAmmoText;

    private WeaponComponent EquippedWeapon;

    // Start is called before the first frame update
    void OnEnable()
    {
        PlayerEvents.OnWeaponEquipped += OnWeaponEquipped;
    }

    void OnDisable()
    {
        PlayerEvents.OnWeaponEquipped -= OnWeaponEquipped;
    }

    private void OnWeaponEquipped(WeaponComponent weapon)
    {
        EquippedWeapon = weapon;
        WeaponNameText.text = weapon.WeaponStats.Name;

    }

    // Update is called once per frame
    void Update()
    {
        if (EquippedWeapon == null) return;

        CurrentClipText.text = EquippedWeapon.WeaponStats.BulletsInClip.ToString();
        TotalAmmoText.text = EquippedWeapon.WeaponStats.TotalBulletsAvailable.ToString();
    }
}
