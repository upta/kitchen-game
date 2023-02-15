using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryCounter : CounterBase
{
    public override void Interact(Player player)
    {
        if (player.Placeable is PlatePlaceable plate)
        {
            PlaceableManager.Instance.Remove(plate);
        }
    }
}
