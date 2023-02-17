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

    private void GameStateManager_OnStateChanged(object sender, GameStateManager.State state)
    {
        gameObject.SetActive(state == GameStateManager.State.Playing);
    }

    private void Update()
    {
        timerValue.fillAmount = GameStateManager.Instance.PlayingTimeNormalized;
    }
}
