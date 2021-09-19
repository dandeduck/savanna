using UnityEngine;

public abstract class Item : MonoBehaviour
{
    [SerializeField] private string type;
    [SerializeField] private int initialAmount = 0;

    private int amount;
    
    private void Start()
    {
        amount = initialAmount;

        OnStart();
    }

    public int Amount()
    {
        return amount;
    }

    public string Type()
    {
        return type;
    }

    public Item Pickup()
    {
        OnPickup();

        return this;
    }

    public Item Drop(int amountDropped, Vector3 dropPosition)
    {
        amountDropped = Mathf.Min(amountDropped, amount);
        amount -= amountDropped;

        Item item = Instantiate<Item>(this, dropPosition, Quaternion.Euler(0,0,0));
        item.SetAmount(amountDropped);

        OnDrop(item);

        if (amount <= 0)
            Destroy(this.gameObject, Time.deltaTime);

        return item;
    }

    public void AddAmount(int amount)
    {
        SetAmount(this.amount + amount);
    }

    public void SetAmount(int amount)
    {
        this.amount = amount;
    }

    public void ReduceAmount(int amount)
    {
        this.amount -= amount;

        if (amount <= 0)
            Destroy(this, Time.deltaTime);
    }

    public void Merge(Item item)
    {
        amount += item.Amount();

        Destroy(item.gameObject, Time.deltaTime);
    }

    public abstract void Use(Vector3 position, Quaternion rotation);
    protected virtual void OnStart() {}
    protected abstract void OnPickup();
    protected abstract void OnDrop(Item droppedItem);

    public override bool Equals(object obj)
    {   
        if (obj == null || GetType() != obj.GetType())
            return false;

        return Equals((Item) obj);
    }

    public bool Equals(Item item)
    {
        return type == item.type && amount == item.amount;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return $"{amount} {type}(s)";
    }
}
