using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CompletePlateVisual : MonoBehaviour
{
    [Serializable]
    public struct Placeable_GameObject
    {
        public PlaceableSO scriptableObject;
        public GameObject gameObject;
    }

    [SerializeField]
    private PlatePlaceable plate;

    [SerializeField]
    private List<Placeable_GameObject> map;

    private void Start()
    {
        plate.OnIngredientAdded += Plate_OnIngredientAdded;

        map.ForEach(a => a.gameObject.SetActive(false));
    }

    private void Plate_OnIngredientAdded(object sender, PlaceableSO so)
    {
        var visual = map.Where(a => a.scriptableObject == so)
            .Cast<Placeable_GameObject?>()
            .SingleOrDefault();

        visual?.gameObject.SetActive(true);
    }
}
