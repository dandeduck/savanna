using UnityEngine;

public class Recipe : MonoBehaviour
{
    [SerializeField] private Item[] ingredients;
    [SerializeField] private int[] ingredientAmounts;
    [SerializeField] private Item result;
    [SerializeField] private bool isLocked;

    private void Awake()
    {
        for (int i = 0; i < ingredients.Length; i++)
        {
            ingredients[i].SetAmount(ingredientAmounts[i]);
        }
    }

    public bool CanCraft(Inventory inventory)
    {
        foreach (Item ingredient in ingredients)
        {
            if (!inventory.ContainsExact(ingredient))
                return false;
        }

        return true;
    }

    public Item Craft(Inventory inventory)
    {
        if (isLocked || !CanCraft(inventory))
            return null;
        
        foreach (Item ingredient in ingredients)
        {
            inventory.Consume(ingredient);
        }

        return Instantiate(result, inventory.transform.position, inventory.transform.rotation);
    }

    public void Unlock()
    {
        isLocked = false;
    }

    public bool IsLocked()
    {
        return isLocked;
    }
}
