using System.Collections.Generic;
using UnityEngine;

public class PlaceableManager : MonoBehaviour
{
    public static PlaceableManager Instance;

    private readonly Dictionary<Placeable, IPlaceableHolder> owners = new();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Trying to create another instance of {nameof(PlaceableManager)}");
        }

        Instance = this;
    }

    public void Claim(Placeable placeable, IPlaceableHolder newOwner)
    {
        var previousOwner = owners.GetValueOrDefault(placeable, null);
        owners[placeable] = newOwner;

        if (previousOwner != null)
        {
            previousOwner.Placeable = null;
        }

        newOwner.Placeable = placeable;

        placeable.transform.parent = newOwner.TargetTransform;
        placeable.transform.localPosition = Vector3.zero;
        placeable.transform.localRotation = Quaternion.identity;
    }

    public Placeable Add(Transform prefab)
    {
        var instance = Instantiate(prefab);
        instance.localPosition = Vector3.zero;

        var placeable = instance.GetComponent<Placeable>();

        owners.Add(placeable, null);

        return placeable;
    }

    public void Remove(Placeable placeable)
    {
        owners.Remove(placeable);
        Destroy(placeable.gameObject);
    }
}
