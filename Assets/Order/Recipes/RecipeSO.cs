using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class RecipeSO : ScriptableObject
{
    public List<PlaceableSO> ingredients;

    public string recipeName;
}
