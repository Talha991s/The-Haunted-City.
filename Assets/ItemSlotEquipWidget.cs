using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotEquipWidget : MonoBehaviour
{
    EquippableScriptable Equippable;
    [SerializeField] private Image EnabledImage;

    private void Awake()
    {
        HideWidget();
    }

    public void ShowWidget()
    {
        gameObject.SetActive(true);
    }

    public void HideWidget()
    {
        gameObject.SetActive(false);
    }

    internal void Initialize(ItemScriptables item)
    {
        if (!(item is EquippableScriptable eqItem)) return;

        Equippable = eqItem;
        ShowWidget();
        Equippable.OnEquipStatusChange += OnEquipmentChange;
        OnEquipmentChange();
    }

    private void OnEquipmentChange()
    {
        EnabledImage.gameObject.SetActive(Equippable.Equipped);
    }

    private void OnDisable()
    {
        if (Equippable) Equippable.OnEquipStatusChange -= OnEquipmentChange;
    }
}
