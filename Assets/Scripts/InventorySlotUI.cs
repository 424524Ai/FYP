using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemName;
    [SerializeField] private GameObject selectionHighlight;

    public void SetItem(ItemData item)
    {
        itemName.text = item != null ? item.itemName : string.Empty;
        itemIcon.sprite = item != null ? item.icon : null;
        itemIcon.enabled = itemIcon.sprite != null;
    }

    public void SetSelected(bool selected)
    {
        if (selectionHighlight != null) selectionHighlight.SetActive(selected);
    }
}
