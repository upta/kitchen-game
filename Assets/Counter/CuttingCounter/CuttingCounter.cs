using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : CounterBase
{
    public override void Interact(Player player)
    {
        if (HasPlaceable)
        {
            if (!player.HasPlaceable)
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

    public override void InteractAlternate(Player player)
    {
        if (HasPlaceable && Placeable.ScriptableObject.alternatePrefab != null)
        {
            Debug.Log("cut");
            PlaceableManager.Instance.Remove(Placeable);

            var placeable = PlaceableManager.Instance.Add(
                Placeable.ScriptableObject.alternatePrefab
            );

            PlaceableManager.Instance.Claim(placeable, this);
        }
    }
}
