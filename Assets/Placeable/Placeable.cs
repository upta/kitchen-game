using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Placeable : MonoBehaviour
{
    [SerializeField]
    private PlaceableSO scriptableObject;
    public PlaceableSO ScriptableObject => scriptableObject;
}
