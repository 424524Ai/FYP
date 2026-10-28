using UnityEngine;

// Represents a collectible item placed in the game world.
public class ItemPickup : MonoBehaviour, IInteractable
{
    [SerializeField] private ItemData itemData;
    [SerializeField] private Inventory playerInventory;

    public void Interact()
    {
        if(itemData == null || playerInventory == null)
        {
            Debug.LogWarning("Item data or player inventory is missing");
            return;
        }

        playerInventory.AddItem(itemData);

        Debug.Log("Picked up: " + itemData.itemName);

        Destroy(gameObject);
    }
}
