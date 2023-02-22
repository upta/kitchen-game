using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }

    public event EventHandler OnInteract;
    public event EventHandler OnInteractAlternate;
    public event EventHandler OnPause;
    public event EventHandler<Binding> OnBinding;
    public event EventHandler<Binding> OnBindingComplete;

    private const string PREF_KEY = "Bindings";

    private PlayerInputActions actions;
    private Dictionary<Binding, BindingAction> bindings;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Trying to create another instance of {nameof(InputManager)}");
        }

        Instance = this;

        actions = new PlayerInputActions();

        if (PlayerPrefs.HasKey(PREF_KEY))
        {
            actions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PREF_KEY));
        }

        actions.Player.Enable();

        actions.Player.Interact.performed += Interact_performed;
        actions.Player.InteractAlternate.performed += InteractAlternate_performed;
        actions.Player.Pause.performed += Pause_performed;

        bindings = new()
        {
            { Binding.MoveUp, new(actions.Player.Move, 1) },
            { Binding.MoveDown, new(actions.Player.Move, 2) },
            { Binding.MoveLeft, new(actions.Player.Move, 3) },
            { Binding.MoveRight, new(actions.Player.Move, 4) },
            { Binding.Interact, new(actions.Player.Interact, 0) },
            { Binding.InteractAlternate, new(actions.Player.InteractAlternate, 0) },
            { Binding.Pause, new(actions.Player.Pause, 0) },
            { Binding.GamepadInteract, new(actions.Player.Interact, 1) },
            { Binding.GamepadInteractAlternate, new(actions.Player.InteractAlternate, 1) },
            { Binding.GamepadPause, new(actions.Player.Pause, 1) },
        };
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

    public string BindingText(Binding binding)
    {
        return bindings[binding].Binding.ToDisplayString();
    }

    public void Rebind(Binding binding)
    {
        OnBinding?.Invoke(this, binding);

        actions.Player.Disable();

        var bind = bindings[binding];

        bind.Action
            .PerformInteractiveRebinding(bind.Index)
            .OnComplete(callback =>
            {
                bind.Refresh();

                callback.Dispose();
                actions.Player.Enable();

                OnBindingComplete?.Invoke(this, binding);

                PlayerPrefs.SetString(PREF_KEY, actions.SaveBindingOverridesAsJson());
                PlayerPrefs.Save();
            })
            .Start();
    }

    private void Interact_performed(InputAction.CallbackContext obj)
    {
        if (
            GameStateManager.Instance.State
            is GameStateManager.GameState.WaitingToStart
                or GameStateManager.GameState.Playing
        )
        {
            OnInteract?.Invoke(this, EventArgs.Empty);
        }
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

    public enum Binding
    {
        MoveUp,
        MoveDown,
        MoveLeft,
        MoveRight,
        Interact,
        InteractAlternate,
        Pause,
        GamepadInteract,
        GamepadInteractAlternate,
        GamepadPause
    }

    private class BindingAction
    {
        public InputAction Action { get; private set; }

        public InputBinding Binding { get; private set; }
        public int Index { get; private set; }

        public BindingAction(InputAction action, int index)
        {
            Action = action;
            Binding = action.bindings[index];
            Index = index;
        }

        public void Refresh()
        {
            Binding = Action.bindings[Index];
        }
    }
}
