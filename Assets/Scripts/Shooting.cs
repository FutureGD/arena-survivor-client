using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Pool;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed = 20f;
    [SerializeField] private float fireRate = .2f;
    private float nextFireTime;
    private IObjectPool<Bullet> bulletPool;

    void Awake()
    {
        bulletPool = new ObjectPool<Bullet>(
            createFunc: () =>
            {
                var b = Instantiate(bulletPrefab).GetComponent<Bullet>();
                b.Pool = bulletPool;
                return b;
            },
            actionOnGet: b => b.gameObject.SetActive(true),
            actionOnRelease: b => b.gameObject.SetActive(false),
            actionOnDestroy: b => Destroy(b.gameObject),
            defaultCapacity: 20,
            maxSize: 50
        );
    }

    void Update()
    {
        if (Mouse.current.leftButton.isPressed && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Fire();
        }
    }

    void Fire()
    {
        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mouseWorld.z = 0;

        Vector2 dir = (mouseWorld - transform.position).normalized;
        Bullet b = bulletPool.Get();
        b.transform.position = transform.position;
        b.GetComponent<Rigidbody2D>().linearVelocity = dir * bulletSpeed;
    }

}
