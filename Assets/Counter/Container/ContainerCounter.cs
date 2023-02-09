using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class ContainerCounter : CounterBase
{
    public event EventHandler OnInteract;

    public override void Interact(IPlaceableHolder target)
    {
        var instance = Instantiate(placeableSO.prefab);
        instance.localPosition = Vector3.zero;

        placeable = instance.GetComponent<Placeable>();
        placeable.Holder = target;

        OnInteract?.Invoke(this, null);
    }
}
