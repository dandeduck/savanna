using UnityEngine;

public abstract class Pickupable : MonoBehaviour
{
    [SerializeField] private string type;

    private int amount;
    
    private void Start()
    {
        amount = 0;

        OnStart();
    }

    private void Update()
    {
        OnUpdate();
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

    protected virtual void OnStart() {}
    protected virtual void OnUpdate() {}
    protected virtual void OnPickup() {}
}
