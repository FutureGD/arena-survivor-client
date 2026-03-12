using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public IObjectPool<Bullet> Pool { get; set; }
    private float lifetime = 2f;
    private float timer;
    private bool isReleased;

    void OnEnable()
    {
        timer = lifetime;
        isReleased = false;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f) ReturnToPool();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Wall"))
        {
            ReturnToPool();
        }
        else if (other.CompareTag("Enemy"))
        {
            Health enemyHealth = other.GetComponent<Health>();
            if (enemyHealth != null)
            {
                enemyHealth.TakeDamage(25);
            }
            ReturnToPool();
        }
    }

    void ReturnToPool()
    {
        if (isReleased) return;
        isReleased = true;
        if (Pool != null)
        {
            Pool.Release(this);
        }
        else
            Destroy(gameObject);
    }
}