using System;
using System.Xml.Schema;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    private float damageAccumulator;
    [SerializeField] private float damagePerSec = 10;
    void OnCollisionStay2D(Collision2D other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            Health h = other.gameObject.GetComponent<Health>();
            if (h != null)
            {
                h.TakeDamage(damagePerSec * Time.fixedDeltaTime);
                // Debug.Log(h.CurrentHealth); // Damage Recieved
            }
        }
    }
}
