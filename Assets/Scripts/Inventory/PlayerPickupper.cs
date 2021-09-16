using UnityEngine;
using System.Collections.Generic;

public class PlayerPickupper : MonoBehaviour
{
    PlayerInventory inventories;
    InputHandler input;
    Navigator navigator;

    private List<Item> items;
    private Item selected;

    private void Start()
    {
        inventories = GetComponent<PlayerInventory>();
        input = GetComponent<InputHandler>();
        navigator = GetComponent<Navigator>();

        items = new List<Item>();
    }

    private void Update()
    {
        if (input.PickingUp()) 
            selected = GetSelected();

        if (selected != null)
            Pickup();
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
        if (input.HasAim())
        {
            Item aimSelected =  GetAimSelected();

            if (aimSelected != null)
                navigator.Navigate(aimSelected.transform);

            return aimSelected;
        }

        else
            return items[0];
    }

    private Item GetAimSelected()
    {
        RaycastHit[] hits = input.AimRaycastAll();

        for (int i = 0; i < hits.Length; i++)
        {
            Item item = hits[i].transform.GetComponent<Item>();

            if (item != null)
                return item;
        }

        return null;
    }

    private void Pickup()
    {
        if (items.Contains(selected))
        {
            inventories.Pickup(selected);
            items.Remove(selected);

            selected = null;
        }
    }
}
