using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : CounterBase
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
}
