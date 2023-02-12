using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private Image bar;

    [SerializeField]
    private CuttingCounter counter;

    public void Start()
    {
        counter.OnProgressNormalizedChanged += Counter_OnProgressNormalizedChanged;
        bar.fillAmount = 0.0f;
        gameObject.SetActive(false);
    }

    private void Counter_OnProgressNormalizedChanged(object sender, float e)
    {
        bar.fillAmount = e;
        gameObject.SetActive(e != 0.0f && e != 1.0f);
    }
}
