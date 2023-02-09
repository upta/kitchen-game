using UnityEngine;

public abstract class CounterBase : MonoBehaviour, IPlaceableHolder
{
    [SerializeField]
    protected PlaceableSO placeableSO;

    [SerializeField]
    protected Transform itemTarget;

    protected Placeable placeable;

    public Placeable Placeable { get; set; }

    public Transform TargetTransform => itemTarget;

    public abstract void Interact(IPlaceableHolder target);
}
