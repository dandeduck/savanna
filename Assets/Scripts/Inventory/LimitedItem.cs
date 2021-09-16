using UnityEngine;

public abstract class LimitedItem : Collectable
{
    public override void Use(Vector3 position)
    {
        // ((LimitedItem) Drop(1, position)).WhenUsed();
        Drop(1, position);
    }

    protected abstract void WhenUsed();
}
