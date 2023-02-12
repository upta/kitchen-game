using System;
using UnityEngine;

public class CuttingVisual : MonoBehaviour
{
    private const string CUT = "Cut";

    [SerializeField]
    private CuttingCounter counter;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        counter.OnCut += Counter_Cut;
    }

    private void Counter_Cut(object sender, EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}
