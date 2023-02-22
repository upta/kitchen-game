using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI receipeCount;

    private void Start()
    {
        GameStateManager.Instance.OnStateChanged += GameStateManager_OnStateChanged;

        gameObject.SetActive(false);
    }

    private void GameStateManager_OnStateChanged(object sender, GameStateManager.GameState state)
    {
        gameObject.SetActive(state == GameStateManager.GameState.GameOver);

        if (state == GameStateManager.GameState.GameOver)
        {
            receipeCount.text = OrderManager.Instance.SuccessfulOrders.ToString();
        }
    }
}
