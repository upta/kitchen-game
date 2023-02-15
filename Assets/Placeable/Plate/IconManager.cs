using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Icons : MonoBehaviour
{
    [SerializeField]
    private PlatePlaceable plate;

    [SerializeField]
    private Transform iconPrefab;

    private void Start()
    {
        plate.OnIngredientAdded += Plate_OnIngredientAdded;
    }

    private void Plate_OnIngredientAdded(object sender, PlaceableSO so)
    {
        var icon = Instantiate(iconPrefab, transform);

        icon.GetComponent<Icon>().Sprite = so.sprite;
    }
}
