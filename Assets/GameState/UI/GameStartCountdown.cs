using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdown : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI label;

    private void Start()
    {
        GameStateManager.Instance.OnStateChanged += GameStateManager_OnStateChanged;

        gameObject.SetActive(false);
    }

    private void Update()
    {
        label.text = Mathf.Ceil(GameStateManager.Instance.Timer).ToString();
    }

    private void GameStateManager_OnStateChanged(object sender, GameStateManager.State state)
    {
        gameObject.SetActive(state == GameStateManager.State.CountdownToStart);
    }
}
