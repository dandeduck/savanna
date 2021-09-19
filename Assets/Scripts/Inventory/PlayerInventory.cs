using UnityEngine;
using System;

public class PlayerInventory : Inventory
{
    [SerializeField] private int lowerInventorySize;

    private InputHandler input;
    private int selectedIndex;

    protected override void OnAwake()
    {
        input = GetComponent<InputHandler>();
        selectedIndex = 0;
    }

    protected override void OnUpdate()
    {
        if (input.ChangingSelectedItem())
            selectedIndex = input.SelectedItem();

        if (input.UsingItem())
            UseSelectedItem(Quaternion.LookRotation(input.AimDirection() - transform.position));
    }

    public Item[] LowerInventory()
    {
        return new ArraySegment<Item>(Items(), Size()-lowerInventorySize, lowerInventorySize).Array;
    }

    private void UseSelectedItem()
    {
        UseSelectedItem(Quaternion.Euler(Vector3.zero));
    }

    private void UseSelectedItem(Quaternion rotation)
    {
        UseSelectedItem(transform.position, rotation);
    }

    private void UseSelectedItem(Vector3 position, Quaternion rotation)
    {
        Item selectedItem = Items()[selectedIndex];

        if (selectedItem == null)
            return;

        selectedItem.Use(position, rotation);
    }
    
    public Item SelectedItem()
    {
        return Items()[selectedIndex];
    }
}
