using System;

public class TrashCounter : CounterBase
{
    public static EventHandler<TrashCounter> OnTrashed;

    public override void Interact(Player player)
    {
        if (player.HasPlaceable)
        {
            PlaceableManager.Instance.Remove(player.Placeable);
            OnTrashed?.Invoke(this, this);
        }
    }
}
