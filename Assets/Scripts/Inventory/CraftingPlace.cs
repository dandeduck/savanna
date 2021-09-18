using UnityEngine;

public class CraftingPlace : MonoBehaviour
{
    [SerializeField] private GameObject recipesContainer;

    private Recipe[] recipes;

    private void Awake()
    {
        if (recipesContainer != null)
            recipes = recipesContainer.GetComponents<Recipe>();
        else
            recipes = GetComponents<Recipe>();
    }

    public Recipe[] Recipes()
    {
        return recipes;
    }
}
