using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundToggle : MonoBehaviour
{
    [SerializeField]
    private StoveCounter counter;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        counter.OnCooking += Counter_OnCooking;
    }

    private void Counter_OnCooking(object sender, bool isCooking)
    {
        if (isCooking)
        {
            audioSource.Play();
        }
        else
        {
            audioSource.Pause();
        }
    }
}
