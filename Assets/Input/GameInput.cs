using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions actions;

    public event EventHandler OnInteract;
    public event EventHandler OnInteractAlternate;

    private void Awake()
    {
        actions = new PlayerInputActions();
        actions.Player.Enable();

        actions.Player.Interact.performed += Interact_performed;
        actions.Player.InteractAlternate.performed += InteractAlternate_performed;
    }

    public Vector2 GetMovementVectorNormalized()
    {
        var input = actions.Player.Move.ReadValue<Vector2>();

        input = input.normalized;

        return input;
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        OnInteract?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(InputAction.CallbackContext obj)
    {
        OnInteractAlternate?.Invoke(this, EventArgs.Empty);
    }
}
