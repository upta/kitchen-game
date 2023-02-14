using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : CounterBase
{
    public event EventHandler OnPlateAdded;
    public event EventHandler OnPlateRemoved;

    private const float MAX_SECONDS = 1.0f;
    private const int MAX_PLATES = 4;

    [SerializeField]
    private PlaceableSO plateSO;

    private float timer;
    private int plateCount;

    public override void Interact(Player player)
    {
        if (player.HasPlaceable || plateCount == 0)
        {
            return;
        }

        var plate = PlaceableManager.Instance.Add(plateSO.prefab);
        PlaceableManager.Instance.Claim(plate, player);

        plateCount--;
        OnPlateRemoved?.Invoke(this, null);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= MAX_SECONDS)
        {
            timer = 0.0f;

            if (plateCount < MAX_PLATES)
            {
                plateCount++;
                OnPlateAdded?.Invoke(this, null);
            }
        }
    }
}
