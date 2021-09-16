using UnityEngine;

public abstract class LimitedItem : Collectable
{
    public override void Use(Vector3 position, Quaternion rotation)
    {
        ((LimitedItem) Drop(1, position)).WhenUsed(position, rotation);
    }

    protected abstract void WhenUsed(Vector3 position, Quaternion rotation);
}
