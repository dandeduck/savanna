using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int size;

    private Dictionary<string, Pickupable> items;
    
    private void Start()
    {
        items = new Dictionary<string, Pickupable>();
    }

    public bool Pickup(Pickupable pickedUp)
    {
        if (items.Count == size)
            return false;

        string type = pickedUp.Type();
        Pickupable sameItem = items[type];

        if (sameItem != null)
            sameItem.Merge(pickedUp);
        else
            items.Add(type, pickedUp.Pickup());

        return true;
    }

    public void Drop(string type, int amountDropped)
    {
        Pickupable item = items[type];

        if (item != null)
        {
            item.Drop(amountDropped, transform.position);

            if (amountDropped == item.Amount())
                items.Remove(type);
        }
    }

    public bool Consume(string type, int amount)
    {
        Pickupable item = items[type];

        if (item != null)
        {
            if (amount > item.Amount())
                return false;

            item.ReduceAmount(amount);

            if (amount == item.Amount())
                items.Remove(type);
            
            return true;
        }

        return false;
    }
}
