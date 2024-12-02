using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private EntityStats stats;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider easeHealthSlider;
    [SerializeField] private TextMeshProUGUI healthText;
    private void Start()
    {
        healthSlider.maxValue = stats.health.GetValue();
        easeHealthSlider.maxValue = stats.health.GetValue();
        healthText.text = stats.GetHealthText();
    }

    private void Update()
    {
        healthSlider.value = stats.currentHealth;
        easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, stats.currentHealth, 0.05f);
        healthText.text = stats.GetHealthText();
    }
}
