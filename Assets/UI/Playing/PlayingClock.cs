using UnityEngine;
using UnityEngine.UI;

public class PlayingClock : MonoBehaviour
{
    [SerializeField]
    private Image timerValue;

    private void Start()
    {
        GameStateManager.Instance.OnStateChanged += GameStateManager_OnStateChanged;

        gameObject.SetActive(false);
    }

    private void GameStateManager_OnStateChanged(object sender, GameStateManager.GameState state)
    {
        gameObject.SetActive(state == GameStateManager.GameState.Playing);
    }

    private void Update()
    {
        timerValue.fillAmount = 1.0f - GameStateManager.Instance.PlayingTimeNormalized;
    }
}
