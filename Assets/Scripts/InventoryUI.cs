using UnityEngine;
using UnityEngine.InputSystem;

// controls the visibility of the inventory UI
public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerLook playerLook;
    [SerializeField] private PlayerInteraction playerInteraction;

    [SerializeField] private Inventory playerInventory;
    [SerializeField] private InventorySlotUI slotPrefab;
    [SerializeField] private Transform slotContainer;

    private bool isOpen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inventoryPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Keyboard.current != null && Keyboard.current.tabKey.wasPressedThisFrame)
        {
            ToggleInventory();
        }
    }

    private void ToggleInventory()
    {
        isOpen = !isOpen;
        inventoryPanel.SetActive(isOpen);

        playerMovement.enabled = !isOpen;
        playerLook.enabled = !isOpen;
        playerInteraction.enabled = !isOpen;

        if (isOpen)
        {

            RefreshInventory();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void RefreshInventory()
    {
        Debug.Log("Inventory item count: " + playerInventory.Items.Count);
        // Remove previously generated slots
        foreach (Transform child in slotContainer)
        {
            Destroy(child.gameObject);
        }
        // create a slot for each collected item
        foreach(ItemData item in playerInventory.Items)
        {
            InventorySlotUI slot = Instantiate(
                slotPrefab, slotContainer);

            slot.SetItem(item);
        }
    }
}
