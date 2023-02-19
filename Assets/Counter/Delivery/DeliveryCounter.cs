using System;

public class DeliveryCounter : CounterBase
{
    public static event EventHandler<DeliveryCounter> OnDeliverySucceeded;
    public static event EventHandler<DeliveryCounter> OnDeliveryFailed;

    private void Awake()
    {
        OnDeliverySucceeded = null;
        OnDeliveryFailed = null;
    }

    public override void Interact(Player player)
    {
        if (player.Placeable is PlatePlaceable plate)
        {
            var success = OrderManager.Instance.Deliver(plate);

            if (success)
            {
                OnDeliverySucceeded?.Invoke(this, this);
            }
            else
            {
                OnDeliveryFailed?.Invoke(this, this);
            }

            PlaceableManager.Instance.Remove(plate);
        }
    }
}
