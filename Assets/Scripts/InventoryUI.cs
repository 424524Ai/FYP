using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    // Preserve serialized references from the previous modal inventory.
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerLook playerLook;
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private InventorySlotUI slotPrefab;
    [SerializeField] private Transform slotContainer;
    [SerializeField] private TMP_Text inventoryFullMessage;

    private readonly InventorySlotUI[] slots = new InventorySlotUI[Inventory.Capacity];
    private float messageUntil;

    // Inspection can block selection without hiding the hotbar or stopping feedback.
    public bool SelectionInputEnabled { get; set; } = true;

    private void OnEnable()
    {
        if (playerInventory == null) return;
        playerInventory.Changed += RefreshInventory;
        playerInventory.InventoryFull += ShowInventoryFull;
        RefreshInventory();
    }

    private void OnDisable()
    {
        if (playerInventory == null) return;
        playerInventory.Changed -= RefreshInventory;
        playerInventory.InventoryFull -= ShowInventoryFull;
    }

    private void Start()
    {
        if (inventoryPanel == null || playerInventory == null || slotPrefab == null || slotContainer == null)
        {
            Debug.LogError("Hotbar references are missing.", this);
            enabled = false;
            return;
        }
        inventoryPanel.SetActive(true);
        for (int i = 0; i < slots.Length; i++)
            slots[i] = Instantiate(slotPrefab, slotContainer);
        if (inventoryFullMessage != null) inventoryFullMessage.gameObject.SetActive(false);
        RefreshInventory();
    }

    private void Update()
    {
        if (SelectionInputEnabled && Mouse.current != null)
        {
            float scroll = Mouse.current.scroll.ReadValue().y;
            // One step per input frame, independent of device-specific wheel units.
            if (scroll != 0f) playerInventory.CycleSelection(scroll > 0f ? -1 : 1);
        }
        if (inventoryFullMessage != null && inventoryFullMessage.gameObject.activeSelf && Time.unscaledTime >= messageUntil)
            inventoryFullMessage.gameObject.SetActive(false);
    }

    private void RefreshInventory()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i] == null) continue;
            slots[i].SetItem(i < playerInventory.Items.Count ? playerInventory.Items[i] : null);
            slots[i].SetSelected(i == playerInventory.SelectedIndex);
        }
    }

    private void ShowInventoryFull()
    {
        if (inventoryFullMessage == null) return;
        inventoryFullMessage.text = "Inventory Full";
        inventoryFullMessage.gameObject.SetActive(true);
        messageUntil = Time.unscaledTime + 2f;
    }
}
