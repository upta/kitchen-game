using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour, IPlaceableHolder
{
    [SerializeField]
    private PlaceableSO placeableSO;

    [SerializeField]
    private Transform itemTarget;

    private Placeable placeable;

    public Placeable Placeable { get; set; }

    public Transform TargetTransform => itemTarget;

    public void Interact(IPlaceableHolder target)
    {
        if (placeable == null)
        {
            var instance = Instantiate(placeableSO.prefab, itemTarget);
            instance.localPosition = Vector3.zero;

            placeable = instance.GetComponent<Placeable>();
            placeable.Holder = this;
        }
        else
        {
            Debug.Log(placeable.Holder);

            placeable.Holder = target;
        }
    }
}
