using UnityEngine;
using System.Collections.Generic;

public class PlayerPickupper : MonoBehaviour
{
    PlayerInventory inventory;
    InputHandler input;
    Navigator navigator;

    private List<Item> items;

    private void Start()
    {
        inventory = GetComponent<PlayerInventory>();
        input = GetComponent<InputHandler>();
        navigator = GetComponent<Navigator>();

        items = new List<Item>();
    }

    private void Update()
    {
        if (input.PickingUp()) 
            Pickup(GetSelected());
    }

    private void OnTriggerEnter(Collider collider)
    {
        Item item = collider.GetComponent<Item>();

        if (item != null)
            items.Add(item);
    }

    private void OnTriggerExit(Collider collider)
    {
        Item item = collider.GetComponent<Item>();

        if (item != null)
            items.Remove(item);
    }

    private Item GetSelected()
    {
        if (items.Count > 0)
            return items[0];
        
        return null;
    }

    private void Pickup(Item selected)
    {
        if (CanPickup(selected))
            HandlePickup(selected);
    }

    private bool CanPickup(Item item)
    {
        return item != null && items.Contains(item);
    }

    private void HandlePickup(Item selected)
    {
        inventory.Pickup(selected);
        items.Remove(selected);
    }
}
