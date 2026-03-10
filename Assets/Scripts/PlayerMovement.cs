using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Input_Actions inputAction;
    public Rigidbody2D rb;
    private Vector2 moveInput;

    void Awake()
    {
        // rb = GetComponent<Rigidbody2D>();
        inputAction = new Input_Actions();
    }
    void OnEnable()
    {
        inputAction.Player.Enable();
    }
    void OnDisable()
    {
        inputAction.Player.Disable();
    }
    void Update()
    {
        moveInput = inputAction.Player.Movement.ReadValue<Vector2>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + moveInput * moveSpeed * Time.fixedDeltaTime);
    }
}
