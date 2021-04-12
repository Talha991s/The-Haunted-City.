using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;

public class ItemPickUpComponent : MonoBehaviour
{
    [SerializeField] private ItemScriptables PickUpItem;

    [Tooltip("Manual Override for Drop Amount, if left at -1 it will use the amount from the scriptable object.")]
    [SerializeField] private int Amount = -1;

    [SerializeField] private MeshRenderer PropMeshRenderer;
    [SerializeField] private MeshFilter PropMeshFilter;
    private ItemScriptables ItemInstance;

    void Awake()
    {
        if (PropMeshRenderer == null) GetComponentInChildren<MeshRenderer>();
        if (PropMeshFilter == null) GetComponentInChildren<MeshFilter>();

        Instantiate();
    }

    public void Instantiate()
    {
        ItemInstance = Instantiate(PickUpItem);
        if (Amount > 0) ItemInstance.SetAmount(Amount);

        ApplyMesh();
    }

    private void ApplyMesh()
    {
        if (PropMeshFilter) 
            PropMeshFilter.mesh = PickUpItem.ItemPrefab.GetComponentInChildren<MeshFilter>().sharedMesh;

        if (PropMeshRenderer) 
            PropMeshRenderer.materials = PickUpItem.ItemPrefab.GetComponentInChildren<MeshRenderer>().sharedMaterials;
    }

    private void OnValidate()
    {
        ApplyMesh();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        //ItemInstance.UseItem(other.GetComponent<PlayerController>());

        Debug.Log($"{PickUpItem.Name} - Picked Up");
        InventoryComponent playerInventory = other.GetComponent<InventoryComponent>();

        if (playerInventory) playerInventory.AddItem(ItemInstance, Amount);


        Destroy(gameObject);
    }
}
