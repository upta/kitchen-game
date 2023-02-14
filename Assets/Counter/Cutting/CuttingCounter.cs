using System;
using System.Linq;
using UnityEngine;

public class CuttingCounter : CounterBase, IHasProgress
{
    public event EventHandler<float> OnProgressNormalizedChanged;
    public event EventHandler OnCut;

    [SerializeField]
    private CuttingRecipeSO[] recipes;

    private int progress;

    public override void Interact(Player player)
    {
        if (HasPlaceable)
        {
            if (player.HasPlaceable)
            {
                if (player.Placeable is PlatePlaceable plate)
                {
                    if (plate.TryAddIngredient(Placeable))
                    {
                        PlaceableManager.Instance.Remove(Placeable);
                    }
                }
            }
            else
            {
                PlaceableManager.Instance.Claim(Placeable, player);
            }
        }
        else
        {
            if (player.HasPlaceable && IsValidPlaceable(player.Placeable))
            {
                PlaceableManager.Instance.Claim(player.Placeable, this);
            }
        }

        progress = 0;
        OnProgressNormalizedChanged?.Invoke(this, 0.0f);
    }

    public override void InteractAlternate(Player player)
    {
        if (HasPlaceable)
        {
            var recipe = recipes.SingleOrDefault(a => a.input == Placeable.ScriptableObject);

            if (recipe != null)
            {
                progress++;

                OnProgressNormalizedChanged?.Invoke(this, (float)progress / recipe.maxCutsRequired);
                OnCut?.Invoke(this, null);

                if (progress >= recipe.maxCutsRequired)
                {
                    PlaceableManager.Instance.Remove(Placeable);

                    var placeable = PlaceableManager.Instance.Add(recipe.output.prefab);

                    PlaceableManager.Instance.Claim(placeable, this);
                }
            }
        }
    }

    public bool IsValidPlaceable(Placeable placeable)
    {
        return recipes.Any(a => a.input == placeable.ScriptableObject);
    }
}
