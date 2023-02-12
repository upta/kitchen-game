using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveOnOffVisual : MonoBehaviour
{
    [SerializeField]
    private StoveCounter counter;

    [SerializeField]
    private GameObject burner;

    [SerializeField]
    private GameObject particles;

    private void Start()
    {
        counter.OnCooking += Counter_OnCooking;
    }

    private void Counter_OnCooking(object sender, bool e)
    {
        burner.SetActive(e);
        particles.SetActive(e);
    }
}
