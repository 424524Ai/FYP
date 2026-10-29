using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Displays the data of an item in an inventory slot
public class InventorySlotUI : MonoBehaviour
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TMP_Text itemName;

    public void SetItem(ItemData item)
    {
        if(item == null)
        {
            return;
        }
        itemName.text = item.itemName;

        if(item.icon != null)
        {
            itemIcon.sprite = item.icon;
            itemIcon.enabled = true;
        }
        else
        {
            itemIcon.enabled = false;
        }
    }
}
