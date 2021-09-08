using UnityEngine;

public class Pickupper : MonoBehaviour
{
    [SerializeField] InputHandler input;

    private void Update()
    {
        Pickupable pickupable = input.AimRaycast().transform.GetComponent<Pickupable>();

        if (pickupable != null)
            pickupable.Pickup();
    }
}
