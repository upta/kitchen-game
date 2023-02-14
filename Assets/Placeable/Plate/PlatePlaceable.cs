using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlatePlaceable : Placeable
{
    public event EventHandler<PlaceableSO> OnIngredientAdded;

    [SerializeField]
    private List<PlaceableSO> validIngredients;

    private readonly List<PlaceableSO> ingredients = new();

    public bool TryAddIngredient(Placeable placeable)
    {
        var so = placeable.ScriptableObject;

        if (ingredients.Contains(so) || !validIngredients.Contains(so))
        {
            return false;
        }

        ingredients.Add(so);
        OnIngredientAdded?.Invoke(this, so);

        return true;
    }
}
