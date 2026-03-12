using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField] private float enemySpeed = 3f;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) playerTransform = player.transform;
    }
    void FixedUpdate()
    {
        if (playerTransform == null) return;
        Vector3 newPos = Vector3.MoveTowards(
            transform.position,
            playerTransform.position,
            enemySpeed * Time.fixedDeltaTime
        );
        // newPos.z = transform.position.z;

        rb.MovePosition(newPos);
        // transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);

    }

}
