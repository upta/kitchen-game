using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private const string PREF_KEY = "MusicVolume";

    private AudioSource audioSource;

    private int volume;
    public int Volume
    {
        get => volume;
        set
        {
            volume = value;
            audioSource.volume = volume * 0.1f;

            PlayerPrefs.SetInt(PREF_KEY, volume);
            PlayerPrefs.Save();
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Trying to create another instance of {nameof(SoundManager)}");
        }

        Instance = this;

        audioSource = GetComponent<AudioSource>();

        Volume = PlayerPrefs.GetInt(PREF_KEY, 5);
    }
}
