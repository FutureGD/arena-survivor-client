using Unity.Mathematics;
using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private GameObject deathEffectPrefab;

    void Start()
    {
        GetComponent<Health>().OnDeath += HandleDeath;
    }

    void HandleDeath()
    {
        Instantiate(deathEffectPrefab, transform.position, quaternion.identity);
        Destroy(this.gameObject);
    }

}
