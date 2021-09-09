using UnityEngine;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Inventory lowbar;

    public bool Pickup(Pickupable pickupable)
    {
        return lowbar.Pickup(pickupable);
    }
}
