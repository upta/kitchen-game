using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visual : MonoBehaviour
{
    private const string OPEN_CLOSE = "OpenClose";

    [SerializeField]
    private ContainerCounter counter;

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        counter.OnInteract += Counter_OnInteract;
    }

    private void Counter_OnInteract(object sender, System.EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
