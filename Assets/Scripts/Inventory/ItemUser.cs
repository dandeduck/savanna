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

    private void Update()
    {
        if (input.UsingItem())
        {
            Item selected = inventory.SelectedItem();

            if (selected != null)
                selected.Use(inventory.UsePosition());
        }
    }
}
