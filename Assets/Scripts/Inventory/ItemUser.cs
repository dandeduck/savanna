using UnityEngine;
using System.Collections;

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
            UseItem();
    }

    private void UseItem()
    {
        Item selected = inventory.SelectedItem();

        if (selected != null)
        {
            selected.Use(inventory.UsePosition(), transform.rotation);
            inventory.RemoveUsedItem(selected);
        }
    }
}
