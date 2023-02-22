using UnityEngine;

[CreateAssetMenu]
public class FryingRecipeSO : ScriptableObject
{
    public PlaceableSO input;

    public PlaceableSO output;

    public float maxFryingSeconds;

    public FryingType type;

    public enum FryingType
    {
        Cooking,
        Burning
    }
}
