using UnityEngine;

[CreateAssetMenu]
public class FryingRecipeSO : ScriptableObject
{
    public PlaceableSO input;

    public PlaceableSO output;

    public float maxFryingSeconds;
}
