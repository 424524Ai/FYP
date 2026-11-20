// Unlocks a door with the selected inventory item without consuming that item.
using UnityEngine;

[DisallowMultipleComponent]
public sealed class LockedDoorInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private Inventory playerInventory;
    [SerializeField] private ItemData requiredItem;
    [SerializeField] private Renderer doorRenderer;
    [SerializeField] private Collider blockingCollider;
    [SerializeField] private InteractionFeedbackUI feedbackUI;

    public bool IsUnlocked { get; private set; }

    public void Interact()
    {
        if (IsUnlocked) return;
        if (playerInventory == null || requiredItem == null || doorRenderer == null ||
            blockingCollider == null || feedbackUI == null)
        {
            Debug.LogWarning("Locked door references are missing.", this);
            return;
        }

        if (playerInventory.SelectedItem != requiredItem)
        {
            feedbackUI.ShowMessage("Requires an access card");
            return;
        }

        IsUnlocked = true;
        doorRenderer.enabled = false;
        blockingCollider.enabled = false;
        feedbackUI.ShowMessage("Door unlocked");
    }
}
