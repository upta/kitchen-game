using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMenu : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(
            GameStateManager.Instance.State is GameStateManager.GameState.WaitingToStart
        );

        GameStateManager.Instance.OnStateChanged += GameStateManager_OnStateChanged;
    }

    private void GameStateManager_OnStateChanged(object sender, GameStateManager.GameState state)
    {
        gameObject.SetActive(state == GameStateManager.GameState.WaitingToStart);
    }
}
