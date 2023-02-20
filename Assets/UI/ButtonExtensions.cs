using TMPro;
using UnityEngine.UI;

static public class ButtonExtensions
{
    static public TextMeshProUGUI Label(this Button button)
    {
        return button.GetComponentInChildren<TextMeshProUGUI>();
    }
}
