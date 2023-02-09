using UnityEngine;

public interface IPlaceableHolder
{
    public Placeable Placeable { get; set; }

    public Transform TargetTransform { get; }
}
