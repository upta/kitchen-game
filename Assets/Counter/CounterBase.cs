using UnityEngine;

public abstract class CounterBase : MonoBehaviour, IPlaceableHolder
{
    [SerializeField]
    protected PlaceableSO placeableSO;

    [SerializeField]
    protected Transform itemTarget;

    public Placeable Placeable { get; set; }

    public Transform TargetTransform => itemTarget;

    public bool HasPlaceable => Placeable != null;

    public abstract void Interact(Player player);
}
