using System;
using TMPro;
using UnityEngine;

public class CountdownMenu : MonoBehaviour
{
    public static CountdownMenu Instance { get; private set; }

    public event EventHandler OnCount;

    private const string TRIGGER_NUMBER_POPUP = "NumberPopup";

    [SerializeField]
    private TextMeshProUGUI label;

    private Animator animator;
    private int prev;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Trying to create another instance of {nameof(CountdownMenu)}");
        }

        Instance = this;

        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        GameStateManager.Instance.OnStateChanged += GameStateManager_OnStateChanged;

        gameObject.SetActive(false);
    }

    private void Update()
    {
        var current = Mathf.CeilToInt(GameStateManager.Instance.Timer);
        label.text = current.ToString();

        if (prev != current)
        {
            prev = current;

            animator.SetTrigger(TRIGGER_NUMBER_POPUP);

            OnCount?.Invoke(this, null);
        }
    }

    private void GameStateManager_OnStateChanged(object sender, GameStateManager.GameState state)
    {
        gameObject.SetActive(state == GameStateManager.GameState.CountdownToStart);
    }
}
