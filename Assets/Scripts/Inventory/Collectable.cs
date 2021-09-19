using UnityEngine;

public abstract class Collectable : Item
{
    private MeshRenderer meshRenderer;
    private Collider objectCollider;

    protected override void OnAwake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        objectCollider = GetComponent<Collider>();
    }

    protected override void OnPickup()
    {
        meshRenderer.enabled = false;
        objectCollider.enabled = false;
    }

    protected override void OnDrop(Item droppedItem)
    {
        MeshRenderer newRenderer = droppedItem.GetComponent<MeshRenderer>();
        Collider newCollider = droppedItem.GetComponent<Collider>();

        newRenderer.enabled = true;
        newCollider.enabled = true;
    }
}
