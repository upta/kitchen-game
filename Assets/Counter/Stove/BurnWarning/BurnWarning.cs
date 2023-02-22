using UnityEngine;

public class BurnWarning : MonoBehaviour
{
    [SerializeField]
    private StoveCounter counter;

    private void Start()
    {
        gameObject.SetActive(false);

        counter.OnProgressNormalizedChanged += Counter_OnProgressNormalizedChanged;
    }

    private void Counter_OnProgressNormalizedChanged(object sender, float progress)
    {
        gameObject.SetActive(counter.IsBurning && progress > 0.5f && progress <= 1.0f);
    }
}
