using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    [SerializeField]
    private PlaceableSO scriptableObject;
    public PlaceableSO ScriptableObject => scriptableObject;

    private IPlaceableHolder holder;
    public IPlaceableHolder Holder
    {
        get => holder;
        set
        {
            holder = value;

            if (holder != null)
            {
                holder.Placeable = this;

                transform.parent = holder.TargetTransform;
                transform.localPosition = Vector3.zero;
            }
        }
    }
}
