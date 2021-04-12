using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

public class InventoryWidget : GameHUDWidget
{
    private ItemDisplayPanel ItemDisplayPanel;

    private List<CategorySelectButton> CategoryButtons;
    private PlayerController PlayerController;

    // Start is called before the first frame update
    void Awake()
    {
        PlayerController = PlayerController.FindObjectOfType<PlayerController>();
        CategoryButtons = GetComponentsInChildren<CategorySelectButton>().ToList<CategorySelectButton>();
        ItemDisplayPanel = GetComponentInChildren<ItemDisplayPanel>();

        foreach (CategorySelectButton button in CategoryButtons)
        {
            button.Initialize(this);
        }
    }

    private void OnEnable()
    {
        if (!PlayerController || !PlayerController.Inventory) return;
        if (PlayerController.Inventory.GetItemCount() <= 0) return;

        ItemDisplayPanel.PopulatePanel(PlayerController.Inventory.GetItemsOfCategory(ItemCategory.None));
    }

    public void SelectCategory(ItemCategory category)
    {
        if (!PlayerController || !PlayerController.Inventory) return;
        if (PlayerController.Inventory.GetItemCount() <= 0) return;

        ItemDisplayPanel.PopulatePanel(PlayerController.Inventory.GetItemsOfCategory(category));
    }
}
