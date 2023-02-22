using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BindDisplay : MonoBehaviour
{
    [SerializeField]
    private InputManager.Binding binding;

    [SerializeField]
    private TextMeshProUGUI label;

    private void Start()
    {
        UpdateUI();

        InputManager.Instance.OnBindingComplete += (_, b) =>
        {
            if (binding == b)
            {
                UpdateUI();
            }
        };
    }

    public void UpdateUI()
    {
        var text = InputManager.Instance.BindingText(binding);

        label.text = text.Length > 3 ? text[..3] : text;
    }
}
