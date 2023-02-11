using System;
using UnityEngine;

public class ContainerCounter : CounterBase
{
    public event EventHandler OnInteract;

    public override void Interact(Player player)
    {
        if (player.HasPlaceable)
        {
            return;
        }

        var placeable = PlaceableManager.Instance.Create(placeableSO);
        PlaceableManager.Instance.Claim(placeable, player);

        OnInteract?.Invoke(this, null);
    }
}
