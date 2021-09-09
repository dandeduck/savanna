using UnityEngine;

public abstract class Pickupable : MonoBehaviour
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

    public Pickupable Pickup()
    {
        OnPickup();

        return this;
    }

    public void Drop(int amountDropped, Vector3 dropPosition)
    {
        amountDropped = Mathf.Min(amountDropped, amount);
        amount -= amountDropped;

        Pickupable pickupable = Instantiate<Pickupable>(this, dropPosition, Quaternion.Euler(0,0,0));
        pickupable.SetAmount(amountDropped);

        OnDrop(pickupable);

        if (amount <= 0)
            Destroy(this.gameObject, Time.deltaTime);
    }

    public void SetAmount(int amount)
    {
        this.amount = amount;
    }

    public void ReduceAmount(int amount)
    {
        this.amount -= amount;

        if (amount <= 0)
            Destroy(this.gameObject, Time.deltaTime);
    }

    public void Merge(Pickupable pickupable)
    {
        amount += pickupable.Amount();

        Destroy(pickupable.gameObject, Time.deltaTime);
    }

    protected virtual void OnStart() {}
    protected abstract void OnPickup();
    protected abstract void OnDrop(Pickupable droppedItem);
}
