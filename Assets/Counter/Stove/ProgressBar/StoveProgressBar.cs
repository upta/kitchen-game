using UnityEngine;

public class StoveProgressBar : MonoBehaviour
{
    private const string TRIGGER_IS_FLASHING = "IsFlashing";

    [SerializeField]
    private StoveCounter counter;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        counter.OnProgressNormalizedChanged += Counter_OnProgressNormalizedChanged;
    }

    private void Counter_OnProgressNormalizedChanged(object sender, float progress)
    {
        animator.SetBool(
            TRIGGER_IS_FLASHING,
            counter.IsBurning && progress > 0.5f && progress <= 1.0f
        );
    }
}
