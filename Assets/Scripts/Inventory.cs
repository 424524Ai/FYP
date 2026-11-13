using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public const int Capacity = 6;
    private readonly List<ItemData> items = new List<ItemData>();
    public IReadOnlyList<ItemData> Items => items;
    public int SelectedIndex { get; private set; }
    public event Action Changed;
    public event Action InventoryFull;

    // Retain the existing entry point for any existing callers.
    public void AddItem(ItemData item) => TryAddItem(item);

    public bool TryAddItem(ItemData item)
    {
        if (item == null) return false;
        if (items.Count >= Capacity)
        {
            InventoryFull?.Invoke();
            return false;
        }
        items.Add(item);
        Changed?.Invoke();
        Debug.Log("Added to inventory: " + item.itemName);
        return true;
    }

    public void CycleSelection(int direction)
    {
        SelectedIndex = (SelectedIndex + direction % Capacity + Capacity) % Capacity;
        Changed?.Invoke();
    }

    public void PrintItems()
    {
        Debug.Log("Inventory contains " + items.Count + " item(s)");
        foreach (ItemData item in items) Debug.Log("- " + item.itemName);
    }
}
