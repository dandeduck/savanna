using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] Inventory lowbar;

    public bool Pickup(Item item)
    {
        return lowbar.Pickup(item);
    }

    public Item SelectedItem()
    {
        return lowbar.SelectedItem();
    }

    public Vector3 UsePosition()
    {
        return lowbar.transform.position;
    }
}
