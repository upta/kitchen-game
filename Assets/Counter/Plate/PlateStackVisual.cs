using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateStack : MonoBehaviour
{
    [SerializeField]
    private Transform itemTarget;

    [SerializeField]
    private Transform prefab;

    [SerializeField]
    private PlateCounter counter;

    private readonly Stack<GameObject> plates = new();

    private void Start()
    {
        counter.OnPlateAdded += Counter_OnPlateAdded;
        counter.OnPlateRemoved += Counter_OnPlateRemoved;
    }

    private void Counter_OnPlateAdded(object sender, EventArgs e)
    {
        var plate = Instantiate(prefab, itemTarget);

        plate.localPosition = new Vector3(0.0f, plates.Count * 0.1f, 0.0f);

        plates.Push(plate.gameObject);
    }

    private void Counter_OnPlateRemoved(object sender, EventArgs e)
    {
        var plate = plates.Pop();

        Destroy(plate);
    }
}
