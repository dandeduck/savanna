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

        if (items.ContainsKey(type))
            items[type].Merge(pickedUp);
        else
            items.Add(type, pickedUp.Pickup());

        return true;
    }

    public void Drop(string type, int amountDropped)
    {
        if (items.ContainsKey(type))
        {
            Pickupable item = items[type];

            item.Drop(amountDropped, transform.position);

            if (amountDropped == item.Amount())
                items.Remove(type);
        }
    }

    public bool Consume(string type, int amount)
    {
        if (items.ContainsKey(type))
        {
            Pickupable item = items[type];

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
