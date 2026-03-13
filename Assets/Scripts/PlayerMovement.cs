using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    private Input_Actions inputAction;
    public Rigidbody2D rb;
    private Vector2 moveInput;
    [SerializeField] private GameObject deathEffectPrefab;

    void Start()
    {
        GetComponent<Health>().OnDeath += FindFirstObjectByType<WaveManager>().PlayerDied;
    }

    // Don't Uncomment its handled on the Wave Manager its just here for keeping history alive.
    // void HandleDeath()
    // {

    //     // StartCoroutine(RestartAfterDelay(2f));
    //     // gameObject.SetActive(false);
    // }

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
