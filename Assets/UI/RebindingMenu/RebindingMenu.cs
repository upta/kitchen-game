using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RebindingMenu : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(false);

        InputManager.Instance.OnBinding += (_, _) => gameObject.SetActive(true);
        InputManager.Instance.OnBindingComplete += (_, _) => gameObject.SetActive(false);
    }
}
