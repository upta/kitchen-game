using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    public event EventHandler<State> OnStateChanged;

    private const float WAITING_DURATION = 1.0f;
    private const float COUNTDOWN_DURATION = 3.0f;
    private const float PLAYING_DURATION = 10.0f;

    private State state = State.WaitingToStart;

    public float Timer { get; private set; } = WAITING_DURATION;

    public bool IsPlaying => state is State.Playing;

    public float PlayingTimeNormalized => Timer / PLAYING_DURATION;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Trying to create another instance of {nameof(OrderManager)}");
        }

        Instance = this;
    }

    private void Update()
    {
        var previousState = state;

        if (state is State.WaitingToStart)
        {
            WaitingToStart();
        }
        else if (state is State.CountdownToStart)
        {
            CountdownToStart();
        }
        else if (state is State.Playing)
        {
            Playing();
        }

        if (state != previousState)
        {
            OnStateChanged?.Invoke(this, state);
        }
    }

    private void WaitingToStart()
    {
        Timer -= Time.deltaTime;

        if (Timer < 0.0f)
        {
            state = State.CountdownToStart;
            Timer = COUNTDOWN_DURATION;
        }
    }

    private void CountdownToStart()
    {
        Timer -= Time.deltaTime;

        if (Timer < 0.0f)
        {
            state = State.Playing;
            Timer = PLAYING_DURATION;
        }
    }

    private void Playing()
    {
        Timer -= Time.deltaTime;

        if (Timer < 0.0f)
        {
            state = State.GameOver;
        }
    }

    public enum State
    {
        WaitingToStart,
        CountdownToStart,
        Playing,
        GameOver
    }
}
