using UnityEngine;
using System.Collections.Generic;

public class Pickupper : MonoBehaviour
{
    [SerializeField] InputHandler input;
    [SerializeField] InventoryManager inventories;

    private List<Pickupable> pickupables;
    private Pickupable selected;

    private void Start()
    {
        pickupables = new List<Pickupable>();
    }

    private void Update()
    {
        if (input.PickingUp())
            selected = GetSelected();

        if (selected != null)
            Pickup();
    }

    private void OnColliderEnter(Collider collider)
    {
        Pickupable pickupable = collider.GetComponent<Pickupable>();

        if (pickupable != null)
            pickupables.Add(pickupable);
    }

    private void OnColliderExit(Collider collider)
    {
        Pickupable pickupable = collider.GetComponent<Pickupable>();

        if (pickupable != null)
            pickupables.Remove(pickupable);
    }

    private Pickupable GetSelected()
    {
        return input.AimRaycast().transform.GetComponent<Pickupable>();
    }

    private void Pickup()
    {
        if (pickupables.Count > 0)
        {
            Pickupable pickupable = pickupables[0];

            inventories.Pickup(pickupable);

            pickupables.Remove(pickupable);
            selected = null;
        }
    }
}
