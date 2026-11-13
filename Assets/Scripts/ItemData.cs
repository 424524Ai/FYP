using UnityEngine;

// Store shared information about a collection item
[CreateAssetMenu(fileName = "NewItem", menuName = "Inventory/Item")]
public class ItemData : ScriptableObject
{
    public string itemId;
    public string itemName;

    [TextArea(2, 4)]
    public string description;

    public Sprite icon;

    [Tooltip("Visual-only prefab used by item inspection; no gameplay components.")]
    public GameObject inspectionPrefab;
}
