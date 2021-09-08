using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    private int amount;
    private string type;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public int Amount()
    {
        return amount;
    }

    public string Type()
    {
        return type;
    }

    public bool IsSameType(InventoryItem item)
    {
        return type == item.Type();
    }

    public void merge(InventoryItem item)
    {
        if (!IsSameType(item))
            throw new System.ArgumentException("Items of different types cannot be merged");
        
        amount += item.amount;
        Destroy(item, Time.deltaTime);
    }
}
