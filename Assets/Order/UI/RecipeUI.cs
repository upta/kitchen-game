using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecipeUI : MonoBehaviour
{
    [SerializeField]
    private Transform iconContainer;

    [SerializeField]
    private Transform iconTemplate;

    [SerializeField]
    private TextMeshProUGUI label;

    private RecipeSO recipe;
    public RecipeSO Recipe
    {
        get => recipe;
        set
        {
            recipe = value;

            label.text = recipe.recipeName;

            foreach (Transform child in iconContainer)
            {
                if (child != iconTemplate)
                {
                    Destroy(child);
                }
            }

            foreach (var ingredient in recipe.ingredients)
            {
                var icon = Instantiate(iconTemplate, iconContainer);
                icon.gameObject.SetActive(true);

                icon.GetComponent<Image>().sprite = ingredient.sprite;
            }
        }
    }

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
}
