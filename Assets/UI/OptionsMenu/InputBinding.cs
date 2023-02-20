using UnityEngine;
using UnityEngine.UI;

[ExecuteAlways]
public class InputRebind : MonoBehaviour
{
    [SerializeField]
    private InputManager.Binding binding;

    [SerializeField]
    private Button button;

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

        button.onClick.AddListener(() =>
        {
            InputManager.Instance.Rebind(binding);
        });
    }

    public void UpdateUI()
    {
        var text = InputManager.Instance.BindingText(binding);

        button.Label().text = text.Length > 3 ? text[..3] : text;
    }
}
