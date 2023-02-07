using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions actions;

    private void Awake()
    {
        actions = new PlayerInputActions();
        actions.Player.Enable();
    }

    public Vector2 GetMovementVectorNomralized()
    {
        var input = actions.Player.Move.ReadValue<Vector2>();

        input = input.normalized;

        return input;
    }
}
