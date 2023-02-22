using System;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance { get; private set; }

    public event EventHandler<GameState> OnStateChanged;

    private const float COUNTDOWN_DURATION = 3.0f;
    private const float PLAYING_DURATION = 300.0f;

    public GameState State { get; set; } = GameState.WaitingToStart;

    public float Timer { get; private set; }

    public bool IsPlaying => State is GameState.Playing;

    public float PlayingTimeNormalized => Timer / PLAYING_DURATION;

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
        InputManager.Instance.OnInteract += InputManager_OnInteract;
    }

    private void InputManager_OnInteract(object sender, EventArgs e)
    {
        if (State == GameState.WaitingToStart)
        {
            Timer = COUNTDOWN_DURATION;
            State = GameState.CountdownToStart;

            OnStateChanged?.Invoke(this, State);
        }
    }

    private void Update()
    {
        var previousState = State;

        if (State is GameState.CountdownToStart)
        {
            CountdownToStart();
        }
        else if (State is GameState.Playing)
        {
            Playing();
        }

        if (State != previousState)
        {
            OnStateChanged?.Invoke(this, State);
        }
    }

    private void CountdownToStart()
    {
        Timer -= Time.deltaTime;

        if (Timer < 0.0f)
        {
            State = GameState.Playing;
            Timer = PLAYING_DURATION;
        }
    }

    private void Playing()
    {
        Timer -= Time.deltaTime;

        if (Timer < 0.0f)
        {
            State = GameState.GameOver;
        }
    }

    public enum GameState
    {
        WaitingToStart,
        CountdownToStart,
        Playing,
        GameOver
    }
}
