using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    private float currentHealth;
    public float CurrentHealth => currentHealth;

    public event Action<float, float> OnHealthChange; // current, max
    public event Action OnDeath;

    void Awake()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth = Mathf.Max(0, currentHealth - amount);
        OnHealthChange?.Invoke(currentHealth, maxHealth);
        if (currentHealth == 0)
            OnDeath?.Invoke();
    }
}
