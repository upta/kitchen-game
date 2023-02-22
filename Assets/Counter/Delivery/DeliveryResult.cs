using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResult : MonoBehaviour
{
    private const string TRIGGER_POPUP = "Popup";

    [SerializeField]
    private Image background;

    [SerializeField]
    private TextMeshProUGUI label;

    [SerializeField]
    private Image icon;

    [SerializeField]
    private Color successColor;

    [SerializeField]
    private Sprite successSprite;

    [SerializeField]
    private Color failColor;

    [SerializeField]
    private Sprite failSprite;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        OrderManager.Instance.OnOrderSucceeded += OrderManager_OnOrderSucceeded;
        OrderManager.Instance.OnOrderFailed += OrderManager_OnOrderFailed;

        gameObject.SetActive(false);
    }

    private void OrderManager_OnOrderSucceeded(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(TRIGGER_POPUP);

        background.color = successColor;
        label.text = $"Delivery{Environment.NewLine}Success";
        icon.sprite = successSprite;
    }

    private void OrderManager_OnOrderFailed(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
        animator.SetTrigger(TRIGGER_POPUP);

        background.color = failColor;
        label.text = $"Delivery{Environment.NewLine}Failed";
        icon.sprite = failSprite;
    }
}
