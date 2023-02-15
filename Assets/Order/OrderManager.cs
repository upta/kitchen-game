using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class OrderManager : MonoBehaviour
{
    public static OrderManager Instance;

    public event EventHandler<List<RecipeSO>> OnActiveRecipesUpdated;

    private const float MAX_TIME = 4.0f;
    private const int MAX_ACTIVE_RECIPES = 4;

    [SerializeField]
    private List<RecipeSO> recipes;

    private List<RecipeSO> activeRecipes = new();

    //public IReadOnlyList<RecipeSO> ActiveRecipes => activeRecipes;

    private float timer;

    public bool Deliver(PlatePlaceable plate)
    {
        var matchingRecipe = recipes.FirstOrDefault(
            recipe => recipe.ingredients.All(a => plate.Ingredients.Contains(a))
        );

        if (matchingRecipe == null)
        {
            return false;
        }

        activeRecipes.Remove(matchingRecipe);

        OnActiveRecipesUpdated?.Invoke(this, activeRecipes);

        return true;
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Trying to create another instance of {nameof(OrderManager)}");
        }

        Instance = this;
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0.0f)
        {
            timer = MAX_TIME;

            if (activeRecipes.Count < MAX_ACTIVE_RECIPES)
            {
                var recipe = recipes[UnityEngine.Random.Range(0, recipes.Count)];
                activeRecipes.Add(recipe);

                OnActiveRecipesUpdated?.Invoke(this, activeRecipes);
            }
        }
    }
}
