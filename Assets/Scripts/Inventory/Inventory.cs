using UnityEngine;
using System.Collections.Generic;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int size;

    private Dictionary<string, Item> items;
    
    private void Start()
    {
        items = new Dictionary<string, Item>();
    }

    public Dictionary<string, Item> Items()
    {
        return items;
    }

    public Item[] ItemsArr()
    {
        Item[] itemsArr = new Item[items.Count];
        items.Values.CopyTo(itemsArr, 0);

        return itemsArr;
    }

    public bool Pickup(Item pickedUp)
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
            Item item = items[type];
            item.Drop(amountDropped, transform.position);

            if (item.Amount() == 0)
                items.Remove(type);
        }
    }

    public bool Consume(string type, int amount)
    {
        if (items.ContainsKey(type))
        {
            Item item = items[type];

            if (amount > item.Amount())
                return false;

            item.ReduceAmount(amount);

            if (amount == item.Amount())
                items.Remove(type);
            
            return true;
        }

        return false;
    }

    public Item SelectedItem()
    {
        Item[] arr = ItemsArr();

        if (arr.Length > 0)
            return arr[0];
        
        return null;
    }

    public void RemoveSelfDroppedItem(string type)
    {
        if (items.ContainsKey(type))
            if (items[type] == null)
                items.Remove(type);
    }
}
