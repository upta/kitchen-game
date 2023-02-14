using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : CounterBase
{
    public override void Interact(Player player)
    {
        if (HasPlaceable)
        {
            if (player.HasPlaceable)
            {
                if (player.Placeable is PlatePlaceable playerPlate)
                {
                    if (playerPlate.TryAddIngredient(Placeable))
                    {
                        PlaceableManager.Instance.Remove(Placeable);
                    }
                }
                else
                {
                    if (Placeable is PlatePlaceable counterPlate)
                    {
                        if (counterPlate.TryAddIngredient(player.Placeable))
                        {
                            PlaceableManager.Instance.Remove(player.Placeable);
                        }
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
            if (player.HasPlaceable)
            {
                PlaceableManager.Instance.Claim(player.Placeable, this);
            }
        }
    }
}
