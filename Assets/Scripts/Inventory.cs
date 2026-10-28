using UnityEngine;
using System.Collections.Generic;

// stores the items collected by the player
public class Inventory : MonoBehaviour
{
    private List<ItemData> items = new List<ItemData>();

    public void AddItem(ItemData item)
    {
        if(item == null)
        {
            return;
        }

        items.Add(item);
        Debug.Log("Added to inventory: " + item.itemName);
    }

    public void PrintItems()
    {
        Debug.Log("Inventory contains " + items.Count + "item(s)");

        foreach(ItemData item in items)
        {
            Debug.Log("- " + item.itemName);
        }
    }
}
