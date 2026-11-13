using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;
using UnityEngine.UI;

// Runs before gameplay Update so F/Esc cannot also drive world controls.
[DefaultExecutionOrder(-1000)]
public sealed class ItemInspectionController : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private InventoryUI hotbar;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private PlayerLook playerLook;
    [SerializeField] private PlayerInteraction playerInteraction;
    [SerializeField] private GameObject interactionCanvas;
    [SerializeField] private GameObject inspectionPanel;
    [SerializeField] private RawImage modelViewport;
    [Tooltip("Full model-view region, including the margins around the aspect-fitted RawImage.")]
    [SerializeField] private RectTransform dragViewport;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private Camera inspectionCamera;
    [SerializeField] private Transform modelPivot;
    [SerializeField, Min(0.01f)] private float dragSensitivity = 0.3f;

    public bool IsInspecting { get; private set; }
    private GameObject modelInstance;
    private bool dragging, restorePending;
    private bool movementWasEnabled, lookWasEnabled, interactionWasEnabled;
    private bool wheelWasEnabled, interactionCanvasWasActive;
    private CursorLockMode previousCursorLock;
    private bool previousCursorVisible;
    private int restoreCursorThroughFrame = -1;

    private void Start()
    {
        if (inspectionPanel != null) inspectionPanel.SetActive(false);
        if (inspectionCamera != null) inspectionCamera.enabled = false;
    }

    private void Update()
    {
        if (restorePending) return;
        var keyboard = Keyboard.current;
        if (!IsInspecting)
        {
            if (keyboard != null && keyboard.fKey.wasPressedThisFrame) TryInspectSelected();
            return;
        }

        if (keyboard != null && keyboard.escapeKey.wasPressedThisFrame)
        {
            CloseInspection();
            return;
        }

        var mouse = Mouse.current;
        if (mouse == null) return;
        if (mouse.leftButton.wasPressedThisFrame)
        {
            var canvas = dragViewport.GetComponentInParent<Canvas>().rootCanvas;
            Camera eventCamera = canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : canvas.worldCamera;
            dragging = RectTransformUtility.RectangleContainsScreenPoint(
                dragViewport, mouse.position.ReadValue(), eventCamera);
        }
        if (!mouse.leftButton.isPressed) dragging = false;
        if (dragging)
        {
            Vector2 delta = mouse.delta.ReadValue() * dragSensitivity;
            modelPivot.Rotate(inspectionCamera.transform.up, -delta.x, Space.World);
            modelPivot.Rotate(inspectionCamera.transform.right, delta.y, Space.World);
        }
    }

    private void LateUpdate()
    {
        // Keep world Update blocked on the Esc frame, then restore its previous state.
        if (restorePending) RestoreInput();
        if (!IsInspecting && Time.frameCount <= restoreCursorThroughFrame)
        {
            // Reassert after Esc processing, including the following editor frame.
            Cursor.lockState = previousCursorLock;
            Cursor.visible = previousCursorVisible;
        }
    }

    public bool TryInspectSelected()
    {
        if (IsInspecting || restorePending || inventory == null) return false;
        int index = inventory.SelectedIndex;
        if (index < 0 || index >= inventory.Items.Count) return false;
        ItemData item = inventory.Items[index];
        if (item == null || item.inspectionPrefab == null)
        {
            Debug.LogWarning("Selected item has no inspection model.", this);
            return false;
        }
        if (!ReferencesReady() || !IsVisualOnly(item.inspectionPrefab))
        {
            Debug.LogWarning("Inspection needs valid scene references and a visual-only mesh prefab.", this);
            return false;
        }

        int layer = LayerMask.NameToLayer("InspectionModel");
        if (layer < 0) return false;
        modelPivot.localRotation = Quaternion.identity;
        modelInstance = Instantiate(item.inspectionPrefab, modelPivot, false);
        modelInstance.name = item.itemName + " (Inspection)";
        foreach (Transform child in modelInstance.GetComponentsInChildren<Transform>(true))
            child.gameObject.layer = layer;
        var renderers = modelInstance.GetComponentsInChildren<MeshRenderer>();
        Bounds bounds = renderers[0].bounds;
        foreach (var renderer in renderers)
        {
            bounds.Encapsulate(renderer.bounds);
            renderer.shadowCastingMode = ShadowCastingMode.Off;
            renderer.receiveShadows = false;
        }
        float diameter = bounds.extents.magnitude * 2f;
        if (diameter <= 0.00001f)
        {
            Destroy(modelInstance);
            modelInstance = null;
            return false;
        }
        // One uniform multiplier preserves every mesh's original proportions.
        modelInstance.transform.localScale *= 1.6f / diameter;
        bounds = renderers[0].bounds;
        foreach (var renderer in renderers) bounds.Encapsulate(renderer.bounds);
        modelInstance.transform.position += modelPivot.position - bounds.center;
        modelPivot.localRotation = Quaternion.Euler(65f, 0f, 0f);

        movementWasEnabled = playerMovement.enabled;
        lookWasEnabled = playerLook.enabled;
        interactionWasEnabled = playerInteraction.enabled;
        wheelWasEnabled = hotbar.SelectionInputEnabled;
        interactionCanvasWasActive = interactionCanvas.activeSelf;
        previousCursorLock = Cursor.lockState;
        previousCursorVisible = Cursor.visible;
        restoreCursorThroughFrame = -1;
        playerMovement.enabled = false;
        playerLook.enabled = false;
        playerInteraction.enabled = false;
        hotbar.SelectionInputEnabled = false;
        interactionCanvas.SetActive(false);
        itemName.text = item.itemName;
        inspectionPanel.SetActive(true);
        inspectionCamera.enabled = true;
        dragging = false;
        IsInspecting = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        return true;
    }

    public void CloseInspection()
    {
        if (!IsInspecting) return;
        HideInspection();
        restorePending = true;
    }

    private void HideInspection()
    {
        IsInspecting = false;
        dragging = false;
        if (modelInstance != null)
        {
            modelInstance.SetActive(false);
            Destroy(modelInstance);
            modelInstance = null;
        }
        if (inspectionPanel != null) inspectionPanel.SetActive(false);
        if (inspectionCamera != null) inspectionCamera.enabled = false;
    }

    private void RestoreInput()
    {
        if (playerMovement != null) playerMovement.enabled = movementWasEnabled;
        if (playerLook != null) playerLook.enabled = lookWasEnabled;
        if (playerInteraction != null) playerInteraction.enabled = interactionWasEnabled;
        if (hotbar != null) hotbar.SelectionInputEnabled = wheelWasEnabled;
        if (interactionCanvas != null) interactionCanvas.SetActive(interactionCanvasWasActive);
        Cursor.lockState = previousCursorLock;
        Cursor.visible = previousCursorVisible;
        restoreCursorThroughFrame = Time.frameCount + 1;
        restorePending = false;
    }

    private bool ReferencesReady()
    {
        return hotbar != null && playerMovement != null && playerLook != null &&
            playerInteraction != null && interactionCanvas != null && inspectionPanel != null &&
            modelViewport != null && dragViewport != null && itemName != null &&
            inspectionCamera != null && modelPivot != null;
    }

    private static bool IsVisualOnly(GameObject prefab)
    {
        // Check before instantiation: gameplay Awake/OnEnable must never run in the preview.
        foreach (Component component in prefab.GetComponentsInChildren<Component>(true))
            if (!(component is Transform) && !(component is MeshFilter) && !(component is MeshRenderer))
                return false;
        return prefab.GetComponentsInChildren<MeshRenderer>().Length > 0;
    }

    private void OnApplicationFocus(bool focused)
    {
        if (!focused) dragging = false;
    }

    private void OnDisable()
    {
        if (!IsInspecting && !restorePending) return;
        HideInspection();
        RestoreInput();
    }
}
