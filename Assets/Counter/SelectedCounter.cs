using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounter : MonoBehaviour
{
    [SerializeField]
    private ClearCounter counter;

    [SerializeField]
    private GameObject visual;

    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Instance_OnSelectedCounterChanged;
    }

    private void Instance_OnSelectedCounterChanged(object sender, ClearCounter e)
    {
        visual.SetActive(e == counter);
    }
}
