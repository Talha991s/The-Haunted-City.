using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplayPanel : MonoBehaviour
{
    [SerializeField] private GameObject ItemSlotPrefab;
    private RectTransform RectTransform;
    private void Awake()
    {
        RectTransform = GetComponent<RectTransform>();
        WipeChildren();
    }

    public void PopulatePanel(List<ItemScriptables> itemList)
    {
        WipeChildren();

        foreach(ItemScriptables item in itemList)
        {
            IconSlot icon = Instantiate(ItemSlotPrefab, RectTransform).GetComponent<IconSlot>();
            icon.Initialize(item);
        }
    }

    // Update is called once per frame
    private void WipeChildren()
    {
        foreach (RectTransform child in RectTransform)
        {
            Destroy(child.gameObject);
        }

        RectTransform.DetachChildren();
    }
}
