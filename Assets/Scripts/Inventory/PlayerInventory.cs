using UnityEngine;
using System;

public class PlayerInventory : Inventory
{
    [SerializeField] private int lowerInventorySize;

    private int selectedIndex;

    public Item[] LowerInventory()
    {
        return new ArraySegment<Item>(Items(), Size()-lowerInventorySize, lowerInventorySize).Array;
    }

    public void UseSelectedItem()
    {
        UseSelectedItem(Quaternion.Euler(Vector3.zero));
    }

    public void UseSelectedItem(Quaternion rotation)
    {
        UseSelectedItem(transform.position, rotation);
    }

    public void UseSelectedItem(Vector3 position, Quaternion rotation)
    {
        Item selectedItem = Items()[selectedIndex];

        if (selectedItem == null)
            return;

        selectedItem.Use(position, rotation);
    }

    public void SelectItem(int index)
    {
        if (Size() >= index+1)
            selectedIndex = index;
    }
    
    public Item SelectedItem()
    {
        return Items()[selectedIndex];
    }
}
