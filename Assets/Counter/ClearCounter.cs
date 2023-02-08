using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField]
    private PlaceableSO placeable;

    [SerializeField]
    private Transform itemTarget;

    public void Interact()
    {
        var instance = Instantiate(placeable.prefab, itemTarget);
        instance.localPosition = Vector3.zero;

        Debug.Log(instance.GetComponent<Placeable>().ScriptableObject.type);
    }
}
