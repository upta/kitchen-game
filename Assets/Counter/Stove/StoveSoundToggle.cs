using System;
using UnityEngine;

public class StoveSoundToggle : MonoBehaviour
{
    public static event EventHandler<StoveCounter> OnWarning;

    private const float MAX_TIMER = 0.2f;

    [SerializeField]
    private StoveCounter counter;

    private AudioSource audioSource;

    private float timer = MAX_TIMER;
    private bool hasBurnWarning = false;

    private void Awake()
    {
        OnWarning = null;

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        counter.OnCooking += Counter_OnCooking;
        counter.OnProgressNormalizedChanged += Counter_OnProgressNormalizedChanged;
    }

    private void Update()
    {
        if (hasBurnWarning)
        {
            timer -= Time.deltaTime;

            if (timer <= 0.0f)
            {
                timer = MAX_TIMER;

                OnWarning?.Invoke(this, counter);
            }
        }
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

    private void Counter_OnProgressNormalizedChanged(object sender, float progress)
    {
        hasBurnWarning = counter.IsBurning && progress > 0.5f && progress <= 1.0f;
    }
}
