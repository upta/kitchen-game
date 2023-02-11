using System;
using UnityEngine;

public class ContainerCounter : CounterBase
{
    [SerializeField]
    protected PlaceableSO placeableSO;

    public event EventHandler OnInteract;

    public override void Interact(Player player)
    {
        if (player.HasPlaceable)
        {
            return;
        }

        var placeable = PlaceableManager.Instance.Add(placeableSO.prefab);
        PlaceableManager.Instance.Claim(placeable, player);

        OnInteract?.Invoke(this, null);
    }
}
