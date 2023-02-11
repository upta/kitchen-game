using System;
using UnityEngine;

public class Player : MonoBehaviour, IPlaceableHolder
{
    public static Player Instance { get; private set; }

    [SerializeField]
    private float moveSpeed = 7f;

    [SerializeField]
    private GameInput gameInput;

    [SerializeField]
    private LayerMask countersLayer;

    [SerializeField]
    private Transform itemTarget;

    private Vector3 lastInteractDirection;
    private CounterBase selectedCounter;

    public event EventHandler<CounterBase> OnSelectedCounterChanged;

    public bool IsWalking { get; private set; }

    public Placeable Placeable { get; set; }

    public Transform TargetTransform => itemTarget;

    public bool HasPlaceable => Placeable != null;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError($"Trying to make a additional {nameof(Player)}");
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        gameInput.OnInteract += GameInput_OnInteract;
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    private void HandleMovement()
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

        bool CanMove(Vector3 direction, float distance)
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

    private void HandleInteraction()
    {
        var input = gameInput.GetMovementVectorNomralized();

        var moveDirection = new Vector3(input.x, 0f, input.y);

        if (moveDirection != Vector3.zero)
        {
            lastInteractDirection = moveDirection;
        }

        var interactDistance = 2.0f;
        selectedCounter = null;

        if (
            Physics.Raycast(
                transform.position,
                lastInteractDirection,
                out var raycastHit,
                interactDistance,
                countersLayer
            )
        )
        {
            if (raycastHit.transform.TryGetComponent(out CounterBase counter))
            {
                selectedCounter = counter;
            }
        }

        OnSelectedCounterChanged?.Invoke(this, selectedCounter);
    }

    private void GameInput_OnInteract(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }
}
