public class TrashCounter : CounterBase
{
    public override void Interact(Player player)
    {
        if (player.HasPlaceable)
        {
            PlaceableManager.Instance.Remove(player.Placeable);
        }
    }
}
