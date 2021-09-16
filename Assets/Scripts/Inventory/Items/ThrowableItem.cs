using UnityEngine;

public class ThrowableItem : LimitedItem
{
    protected override void WhenUsed()
    {
        Instantiate(gameObject, transform.position, transform.rotation).GetComponent<Rigidbody>().AddForce(Vector3.forward, ForceMode.Impulse);
    }
}
