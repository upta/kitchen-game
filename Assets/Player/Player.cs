using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 7f;

    [SerializeField]
    private GameInput gameInput;

    public bool IsWalking { get; private set; }

    private void Update()
    {
        var input = gameInput.GetMovementVectorNomralized();

        var moveDirection = new Vector3(input.x, 0f, input.y);
        var moveDistance = moveSpeed * Time.deltaTime;

        var canMove = CanMove(moveDirection, moveDistance);

        if (canMove)
        {
            transform.position += moveDirection * moveDistance;
        }
        else
        {
            var moveDirectionX = new Vector3(moveDirection.x, 0f, 0f).normalized;

            canMove = CanMove(moveDirectionX, moveDistance);

            if (canMove)
            {
                transform.position += moveDirectionX * moveDistance;
            }
            else
            {
                var moveDirectionZ = new Vector3(0f, 0f, moveDirection.z).normalized;

                canMove = CanMove(moveDirectionZ, moveDistance);

                if (canMove)
                {
                    transform.position += moveDirectionZ * moveDistance;
                }
            }
        }

        var rotateSpeed = 10f;

        transform.forward = Vector3.Slerp(
            transform.forward,
            moveDirection,
            Time.deltaTime * rotateSpeed
        );

        IsWalking = moveDirection != Vector3.zero;
    }

    private bool CanMove(Vector3 direction, float distance)
    {
        var playerRadius = 0.7f;
        var playerHeight = 2.0f;

        return !Physics.CapsuleCast(
            transform.position,
            transform.position + (Vector3.up * playerHeight),
            playerRadius,
            direction,
            distance
        );
    }
}
