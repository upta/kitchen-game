using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlaceableSO : ScriptableObject
{
    public Transform prefab;

    public Transform alternatePrefab;

    public Sprite sprite;

    public string type;
}
