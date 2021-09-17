using UnityEngine;

public class ThrowableItem : LimitedItem
{
    protected override void WhenUsed(Vector3 position, Quaternion rotation)
    {
        AddAmount(1);
        Drop(1, position).GetComponent<Rigidbody>().AddForce(rotation * Vector3.forward * 10, ForceMode.Impulse);
    }
}
