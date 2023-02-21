using System;
using UnityEngine;

public class Player : MonoBehaviour, IPlaceableHolder
{
    public event EventHandler<Player> OnPickUp;
    public event EventHandler<Player> OnDrop;

    public static Player Instance { get; private set; }

    [SerializeField]
    private float moveSpeed = 7f;

    [SerializeField]
    private InputManager inputManager;

    [SerializeField]
    private LayerMask countersLayer;

    [SerializeField]
    private Transform itemTarget;

    private Vector3 lastInteractDirection;
    private CounterBase selectedCounter;

    public event EventHandler<CounterBase> OnSelectedCounterChanged;

    public bool IsWalking { get; private set; }

    private Placeable placeable;
    public Placeable Placeable
    {
        get => placeable;
        set
        {
            placeable = value;

            if (placeable != null)
            {
                OnPickUp?.Invoke(this, this);
            }
            else
            {
                OnDrop?.Invoke(this, this);
            }
        }
    }

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
        inputManager.OnInteract += InputManager_OnInteract;
        inputManager.OnInteractAlternate += InputManager_OnInteractAlternate;
    }

    private void Update()
    {
        HandleMovement();
        HandleInteraction();
    }

    private void HandleMovement()
    {
        var input = inputManager.GetMovementVectorNormalized();

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

            canMove =
                (moveDirection.x < -0.5f || moveDirection.x > 0.5f)
                && CanMove(moveDirectionX, moveDistance);

            if (canMove)
            {
                transform.position += moveDirectionX * moveDistance;
            }
            else
            {
                var moveDirectionZ = new Vector3(0f, 0f, moveDirection.z).normalized;

                canMove =
                    (moveDirection.z < -0.5f || moveDirection.z > 0.5f)
                    && CanMove(moveDirectionZ, moveDistance);

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
        var input = inputManager.GetMovementVectorNormalized();

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

    private void InputManager_OnInteract(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.Interact(this);
        }
    }

    private void InputManager_OnInteractAlternate(object sender, EventArgs e)
    {
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }
}
