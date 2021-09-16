using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] Inventory useBar;

    public bool Pickup(Item item)
    {
        return useBar.Pickup(item);
    }

    public Item SelectedItem()
    {
        return useBar.SelectedItem();
    }

    public Vector3 UsePosition()
    {
        return useBar.transform.position;
    }

    public void RemoveUsedItem(Item item)
    {
        useBar.RemoveSelfDroppedItem(item.Type());
    }
}
