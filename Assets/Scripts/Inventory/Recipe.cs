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
        return CanCraft(inventory, 1);
    }

    public bool CanCraft(Inventory inventory, int amount)
    {
        foreach (Item ingredient in ingredients)
        {
            if (!inventory.ContainsAtLeast(ingredient, ingredient.Amount() * amount))
                return false;
        }

        return true;
    }

    public Item Craft(Inventory inventory, int amount)
    {
        if (isLocked || !CanCraft(inventory, amount))
            return null;
        
        foreach (Item ingredient in ingredients)
        {
            inventory.Consume(ingredient);
        }

        Item output = Instantiate(result, inventory.transform.position, inventory.transform.rotation);
        output.SetAmount(amount);

        return output;
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
