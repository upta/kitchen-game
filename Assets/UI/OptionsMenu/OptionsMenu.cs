using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public static OptionsMenu Instance { get; private set; }

    public event EventHandler<bool> OnVisibilityChanged;

    [SerializeField]
    private Button soundEffects;

    [SerializeField]
    private TextMeshProUGUI soundEffectsText;

    [SerializeField]
    private Button music;

    [SerializeField]
    private TextMeshProUGUI musicText;

    [SerializeField]
    private Button close;

    public bool IsVisible
    {
        get => gameObject.activeInHierarchy;
        set
        {
            gameObject.SetActive(value);

            if (value)
            {
                soundEffects.Select();
            }

            OnVisibilityChanged?.Invoke(this, value);
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Trying to create another instance of {nameof(OptionsMenu)}");
        }

        Instance = this;

        IsVisible = false;
    }

    private void Start()
    {
        soundEffects.onClick.AddListener(() =>
        {
            if (SoundManager.Instance.Volume >= 10)
            {
                SoundManager.Instance.Volume = 0;
            }
            else
            {
                SoundManager.Instance.Volume++;
            }

            UpdateText();
        });

        music.onClick.AddListener(() =>
        {
            if (MusicManager.Instance.Volume >= 10)
            {
                MusicManager.Instance.Volume = 0;
            }
            else
            {
                MusicManager.Instance.Volume++;
            }

            UpdateText();
        });

        close.onClick.AddListener(() =>
        {
            IsVisible = false;
        });

        UpdateText();
    }

    private void UpdateText()
    {
        soundEffectsText.text = $"Sound Effects: {SoundManager.Instance.Volume}";
        musicText.text = $"Music: {MusicManager.Instance.Volume}";
    }
}
