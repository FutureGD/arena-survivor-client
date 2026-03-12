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
        GetComponent<Health>().OnDeath += HandleDeath;
    }

    void HandleDeath()
    {
        Instantiate(deathEffectPrefab, transform.position, quaternion.identity);
        gameObject.SetActive(false);
        StartCoroutine(RestartAfterDelay(2f));
    }

    IEnumerator RestartAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
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
