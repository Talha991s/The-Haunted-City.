using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Character;

public class IconSlot : MonoBehaviour
{
    private ItemScriptables Item;

    private Button ItemButton;
    private TMP_Text ItemText;

    [SerializeField] private ItemSlotAmountWidget AmountWidget;
    [SerializeField] private ItemSlotEquipWidget EquipWidget;

    private void Awake()
    {
        ItemButton = GetComponent<Button>();
        ItemText = GetComponentInChildren<TMP_Text>();
    }

    public void Initialize(ItemScriptables item)
    {
        Item = item;
        ItemText.text = item.Name;
        AmountWidget.Initialize(item);
        EquipWidget.Initialize(item);

        ItemButton.onClick.AddListener(UseItem);
        item.OnItemDestroyed += OnItemDestroyed;
    }

    public void UseItem()
    {
        Item.UseItem(Item.Controller);
    }

    private void OnItemDestroyed()
    {
        Item = null;
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        if (Item) Item.OnItemDestroyed -= OnItemDestroyed;
    }
}
