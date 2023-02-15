using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdersUI : MonoBehaviour
{
    [SerializeField]
    private Transform recipeContainer;

    [SerializeField]
    private Transform recipeTemplate;

    private void Awake()
    {
        recipeTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        OrderManager.Instance.OnActiveRecipesUpdated += Instance_OnActiveRecipesUpdated;
    }

    private void Instance_OnActiveRecipesUpdated(object sender, List<RecipeSO> activeRecipes)
    {
        foreach (Transform child in recipeContainer)
        {
            if (child != recipeTemplate)
            {
                Destroy(child.gameObject);
            }
        }

        foreach (var recipe in activeRecipes)
        {
            var item = Instantiate(recipeTemplate, recipeContainer);
            item.gameObject.SetActive(true);

            item.GetComponent<RecipeUI>().Recipe = recipe;
        }
    }
}
