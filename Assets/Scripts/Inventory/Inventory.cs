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

    public bool Pickup(Item item)
    {
        if (items.Count == size)
            return false;

        string type = item.Type();

        if (items.ContainsKey(type))
            items[type].Merge(item);
        else
            items.Add(type, item.Pickup());

        return true;
    }

    public Item Drop(Item item, int amountDropped)
    {
        return Drop(item.Type(), amountDropped);
    }

    public Item Drop(string type, int amountDropped)
    {
        if (!items.ContainsKey(type))
            return null;

        Item item = items[type];
        Item dropped = item.Drop(amountDropped, transform.position);

        if (item.Amount() == 0)
            items.Remove(type);

        return dropped;
    }

    public bool Consume(Item item)
    {
        return Consume(item, item.Amount());
    }

    public bool Consume(Item item, int amount)
    {
        return Consume(item.Type(), amount);
    }

    public bool Consume(string type, int amount)
    {
        if (!items.ContainsKey(type))
            return false;

        Item item = items[type];

        if (amount > item.Amount())
            return false;

        item.ReduceAmount(amount);

        if (amount == item.Amount())
            items.Remove(type);
        
        return true;
    }

    public Item SelectedItem()
    {
        Item[] arr = ItemsArr();

        if (arr.Length > 0)
            return arr[0];
        
        return null;
    }

    public bool ContainsExact(Item item)
    {
        return items.ContainsValue(item);
    }

    public void ClearUsedItem(Item item)
    {
        ClearUsedItem(item.Type());
    }

    public void ClearUsedItem(string type)
    {
        if (items.ContainsKey(type))
            if (items[type].Amount() <= 0)
                items.Remove(type);
    }
}
