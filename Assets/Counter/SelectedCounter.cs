using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounter : MonoBehaviour
{
    [SerializeField]
    private CounterBase counter;

    [SerializeField]
    private GameObject[] visuals;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Instance_OnSelectedCounterChanged;
    }

    private void Instance_OnSelectedCounterChanged(object sender, CounterBase e)
    {
        foreach (var visual in visuals)
        {
            visual.SetActive(e == counter);
        }
    }
}
