using UnityEngine;

public abstract class Pickupable : MonoBehaviour
{
    [SerializeField] private string type;

    private int amount;
    
    private void Start()
    {
        amount = 0;
    }

    public int Pickup()
    {
        OnPickup();

        return amount;
    }

    public void SetAmount(int amount)
    {
        this.amount = amount;
    }

    protected abstract void OnPickup();
}
