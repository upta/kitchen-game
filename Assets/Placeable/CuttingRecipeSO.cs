using UnityEngine;

[CreateAssetMenu]
public class CuttingRecipeSO : ScriptableObject
{
    public PlaceableSO input;

    public PlaceableSO output;

    public int maxCutsRequired;
}
