using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float runSpeed = 8f;
    [SerializeField] private float rollSpeed = 10f;

    private float initialSpeed;
    private bool _isRunning = false;
    private bool _isRolling = false;

    private Rigidbody2D rb;
    private Vector2 _moveDirection;
    private InputAction moveAction;
    private InputAction runAction;
    private InputAction rollAction;

    public Vector2 moveDirection { get => _moveDirection; set => _moveDirection = value; }
    public bool isRunning { get => _isRunning; set => _isRunning = value; }
    public bool isRolling { get => _isRolling; set => _isRolling = value; }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialSpeed = speed;

        moveAction = new InputAction("Move", InputActionType.Value);
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");

        runAction = new InputAction("Run", InputActionType.Button);
        runAction.AddBinding("<Keyboard>/leftShift");

        rollAction = new InputAction("Roll", InputActionType.Button);
        rollAction.AddBinding("<Keyboard>/space");

        moveAction.Enable();
        runAction.Enable();
        rollAction.Enable();
    }

    private void Update()
    {
        OnRun();
        OnRoll();
    }

    private void FixedUpdate()
    {
        OnInput();
        OnMove();
    }

    private void OnEnable()
    {
        moveAction?.Enable();
        runAction?.Enable();
    }

    private void OnDisable()
    {
        moveAction?.Disable();
        runAction?.Disable();
        rollAction?.Disable();
    }

    private void OnDestroy()
    {
        moveAction?.Dispose();
        runAction?.Dispose();
        rollAction?.Dispose();
    }

    #region Movement

    void OnInput()
    {
        _moveDirection = moveAction.ReadValue<Vector2>();
    }

    void OnMove()
    {
        rb.MovePosition(rb.position + _moveDirection * speed * Time.fixedDeltaTime);
    }

    void OnRun()
    {
        if (runAction.WasPressedThisFrame())
        {
            speed = runSpeed;
            isRunning = true;
        }

        if (runAction.WasReleasedThisFrame())
        {
            speed = initialSpeed;
            isRunning = false;
        }
    }

    void OnRoll()
    {
        if (rollAction.WasPressedThisFrame())
        {
            speed = rollSpeed;
            isRolling = true;
        }

        if (rollAction.WasReleasedThisFrame())
        {
            speed = initialSpeed;
            isRolling = false;
        }
    }

    #endregion
}
