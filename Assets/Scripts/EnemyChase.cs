using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    private Transform playerTransform;
    [SerializeField] private float enemySpeed = 3f;

    void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null) playerTransform = player.transform;
    }
    void FixedUpdate()
    {
        if (playerTransform == null) return;
        Vector2 newPos = Vector2.MoveTowards(
            transform.position,
            playerTransform.position,
            enemySpeed * Time.fixedDeltaTime
        );

        transform.position = new Vector3(newPos.x, newPos.y, transform.position.z);

    }

}
