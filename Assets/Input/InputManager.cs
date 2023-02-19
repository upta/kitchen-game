using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    private PlayerInputActions actions;

    public event EventHandler OnInteract;
    public event EventHandler OnInteractAlternate;
    public event EventHandler OnPause;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Trying to create another instance of {nameof(InputManager)}");
        }

        Instance = this;

        actions = new PlayerInputActions();
        actions.Player.Enable();

        actions.Player.Interact.performed += Interact_performed;
        actions.Player.InteractAlternate.performed += InteractAlternate_performed;
        actions.Player.Pause.performed += Pause_performed;
    }

    private void OnDestroy()
    {
        actions.Player.Interact.performed -= Interact_performed;
        actions.Player.InteractAlternate.performed -= InteractAlternate_performed;
        actions.Player.Pause.performed -= Pause_performed;

        actions.Dispose();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        var input = actions.Player.Move.ReadValue<Vector2>();

        input = input.normalized;

        return input;
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        if (!GameStateManager.Instance.IsPlaying)
        {
            return;
        }

        OnInteract?.Invoke(this, EventArgs.Empty);
    }

    private void InteractAlternate_performed(InputAction.CallbackContext obj)
    {
        if (!GameStateManager.Instance.IsPlaying)
        {
            return;
        }

        OnInteractAlternate?.Invoke(this, EventArgs.Empty);
    }

    private void Pause_performed(InputAction.CallbackContext obj)
    {
        OnPause?.Invoke(this, EventArgs.Empty);
    }
}
