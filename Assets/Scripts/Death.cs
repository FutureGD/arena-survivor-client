using Unity.Mathematics;
using UnityEngine;

public class Death : MonoBehaviour
{
    [SerializeField] private GameObject deathEffectPrefab;

    void Start()
    {
        GetComponent<Health>().OnDeath += HandleDeath;
    }

    void HandleDeath()
    {
        Instantiate(deathEffectPrefab, transform.position, quaternion.identity);
        if (gameObject.CompareTag("Enemy"))
            Destroy(this.gameObject);
    }

}
