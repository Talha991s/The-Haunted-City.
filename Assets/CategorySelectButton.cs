using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CategorySelectButton : MonoBehaviour
{
    [SerializeField] private ItemCategory Category;

    // References
    private Button SelectButton;
    private InventoryWidget InventoryWidget;

    private void Awake()
    {
        SelectButton = GetComponent<Button>();
        SelectButton.onClick.AddListener(OnClick);
    }

    public void Initialize(InventoryWidget inventoryWidget)
    {
        InventoryWidget = inventoryWidget;
    }

    private void OnClick()
    {
        if (!InventoryWidget) return;
        InventoryWidget.SelectCategory(Category);
    }
}
