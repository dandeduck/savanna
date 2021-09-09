using UnityEngine;

public class Collectable : Pickupable
{
    private MeshRenderer meshRenderer;
    private Collider objectCollider;

    protected override void OnStart()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        objectCollider = GetComponent<Collider>();
    }

    protected override void OnPickup()
    {
        meshRenderer.enabled = false;
        objectCollider.enabled = false;
    }

    protected override void OnDrop(Pickupable droppedItem)
    {
        MeshRenderer newRenderer = droppedItem.GetComponent<MeshRenderer>();
        Collider newCollider = droppedItem.GetComponent<Collider>();

        newRenderer.enabled = true;
        newCollider.enabled = true;
    }
}
