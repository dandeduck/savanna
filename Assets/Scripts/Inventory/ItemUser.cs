using UnityEngine;

public class ItemUser : MonoBehaviour
{
    private InputHandler input;
    private PlayerInventory inventory;
    
    private void Awake()
    {
        input = GetComponent<InputHandler>();
        inventory = GetComponent<PlayerInventory>();
    }

    private void Start()
    {
        inventory.SelectItem(0);
    }

    private void Update()
    {
        Debug.Log(inventory.SelectedItem());
        Debug.Log(inventory.SelectedItem() == null);
        if (input.UsingItem())
            inventory.UseSelectedItem(transform.rotation);
    }
}
