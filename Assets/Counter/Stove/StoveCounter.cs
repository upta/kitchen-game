using System;
using System.Linq;
using UnityEngine;

public class StoveCounter : CounterBase, IHasProgress
{
    public event EventHandler<bool> OnCooking;
    public event EventHandler<float> OnProgressNormalizedChanged;

    [SerializeField]
    private FryingRecipeSO[] recipes;

    private float timer;
    private FryingRecipeSO activeRecipe;

    private void Update()
    {
        if (HasPlaceable)
        {
            timer += Time.deltaTime;

            if (activeRecipe != null)
            {
                if (timer > activeRecipe.maxFryingSeconds)
                {
                    PlaceableManager.Instance.Remove(Placeable);

                    var placeable = PlaceableManager.Instance.Add(activeRecipe.output.prefab);

                    PlaceableManager.Instance.Claim(placeable, this);

                    OnProgressNormalizedChanged?.Invoke(
                        this,
                        timer / activeRecipe.maxFryingSeconds
                    );

                    timer = 0;
                    activeRecipe = FindRecipe(Placeable.ScriptableObject);
                }
                else
                {
                    OnProgressNormalizedChanged?.Invoke(
                        this,
                        timer / activeRecipe.maxFryingSeconds
                    );
                }
            }
        }
    }

    public override void Interact(Player player)
    {
        if (HasPlaceable)
        {
            if (!player.HasPlaceable)
            {
                PlaceableManager.Instance.Claim(Placeable, player);

                OnCooking?.Invoke(this, false);

                timer = 0.0f;
                OnProgressNormalizedChanged?.Invoke(this, 0.0f);
            }
        }
        else
        {
            activeRecipe = FindRecipe(player.Placeable?.ScriptableObject);

            if (activeRecipe != null)
            {
                PlaceableManager.Instance.Claim(player.Placeable, this);

                OnCooking?.Invoke(this, true);
            }
        }
    }

    private FryingRecipeSO FindRecipe(PlaceableSO so)
    {
        return recipes.SingleOrDefault(a => a.input == so);
    }
}
