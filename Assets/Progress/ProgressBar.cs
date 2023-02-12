using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    [SerializeField]
    private Image bar;

    [SerializeField]
    private GameObject progressor;

    public void Start()
    {
        var instance = progressor.GetComponent<IHasProgress>();

        if (instance == null)
        {
            Debug.LogError(
                $"{nameof(progressor)} does not have an instance of {nameof(IHasProgress)}"
            );
        }

        instance.OnProgressNormalizedChanged += Progressor_OnProgressNormalizedChanged;
        bar.fillAmount = 0.0f;
        gameObject.SetActive(false);
    }

    private void Progressor_OnProgressNormalizedChanged(object sender, float e)
    {
        e = Mathf.Clamp01(e);

        bar.fillAmount = e;
        gameObject.SetActive(e != 0.0f && e != 1.0f);
    }
}
