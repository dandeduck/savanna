using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] Inventory useBar;

    public bool Pickup(Item item)
    {
        return useBar.Pickup(item);
    }

    public void UseSelectedItem()
    {
        UseSelectedItem(UsePosition(), Quaternion.Euler(Vector3.zero));
    }

    public void UseSelectedItem(Quaternion rotation)
    {
        UseSelectedItem(UsePosition(), rotation);
    }

    public void UseSelectedItem(Vector3 position, Quaternion rotation)
    {
        Item selected = SelectedItem();

        if (selected != null)
        {
            selected.Use(position, rotation);
            useBar.ClearUsedItem(selected.Type());
        }
    }
    
    private Item SelectedItem()
    {
        return useBar.SelectedItem();
    }

    private Vector3 UsePosition()
    {
        return useBar.transform.position;
    }
}
