using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public event EventHandler OnPaused;
    public event EventHandler OnUnpaused;

    public static PauseManager Instance { get; private set; }

    private bool isPaused = false;
    public bool IsPaused
    {
        get => isPaused;
        set
        {
            if (isPaused == value)
            {
                return;
            }

            isPaused = value;

            if (isPaused)
            {
                OnPaused?.Invoke(this, null);

                Time.timeScale = 0.0f;
            }
            else
            {
                OnUnpaused?.Invoke(this, null);

                Time.timeScale = 1.0f;
            }
        }
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Trying to create another instance of {nameof(OrderManager)}");
        }

        Instance = this;
    }

    private void Start()
    {
        InputManager.Instance.OnPause += (_, _) =>
        {
            IsPaused = !IsPaused;
        };
    }
}
