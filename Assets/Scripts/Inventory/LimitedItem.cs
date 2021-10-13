using UnityEngine;

public abstract class LimitedItem : Collectable
{
    public override void Use(Vector3 position, Quaternion rotation)
    {
        WhenUsed(position, rotation);
        ReduceAmount(1);
    }

    protected abstract void WhenUsed(Vector3 position, Quaternion rotation);
}
