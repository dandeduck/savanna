using UnityEngine;
using System.Collections.Generic;

public class PlayerPickupper : MonoBehaviour
{
    PlayerInventoryManager inventories;
    InputHandler input;
    Navigator navigator;

    private List<Pickupable> pickupables;
    private Pickupable selected;

    private void Start()
    {
        inventories = GetComponent<PlayerInventoryManager>();
        input = GetComponent<InputHandler>();
        navigator = GetComponent<Navigator>();

        pickupables = new List<Pickupable>();
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
        Pickupable pickupable = collider.GetComponent<Pickupable>();

        if (pickupable != null)
            pickupables.Add(pickupable);
    }

    private void OnTriggerExit(Collider collider)
    {
        Pickupable pickupable = collider.GetComponent<Pickupable>();

        if (pickupable != null)
            pickupables.Remove(pickupable);
    }

    private Pickupable GetSelected()
    {
        if (input.HasAim())
        {
            Pickupable aimSelected =  GetAimSelected();

            if (aimSelected != null)
                navigator.Navigate(aimSelected.transform);

            return aimSelected;
        }

        else
            return pickupables[0];
    }

    private Pickupable GetAimSelected()
    {
        RaycastHit[] hits = input.AimRaycastAll();

        for (int i = 0; i < hits.Length; i++)
        {
            Pickupable pickupable = hits[i].transform.GetComponent<Pickupable>();

            if (pickupable != null)
                return pickupable;
        }

        return null;
    }

    private void Pickup()
    {
        if (pickupables.Count > 0)
        {
            inventories.Pickup(selected);
            pickupables.Remove(selected);

            selected = null;
        }
    }
}
