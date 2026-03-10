using UnityEngine;
using UnityEngine.Pool;

public class Bullet : MonoBehaviour
{
    public IObjectPool<Bullet> Pool { get; set; }
    private float lifetime = 2f;
    private float timer;

    void OnEnable()
    {
        timer = lifetime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0f) ReturnToPool();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") || other.CompareTag("Wall"))
        {
            ReturnToPool();
        }
    }

    void ReturnToPool()
    {
        if (Pool != null)
        {
            Pool.Release(this);
        }
        else
            Destroy(gameObject);
    }
}