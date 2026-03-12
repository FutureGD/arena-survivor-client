using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Health playerHealth;

    void Start()
    {
        playerHealth.OnHealthChange += (curr, max) => healthSlider.value = curr;
    }
}
