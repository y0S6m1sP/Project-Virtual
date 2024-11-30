using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UI_BossHealthBar : MonoBehaviour
{
    private EntityStats stats;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider easeHealthSlider;
    [SerializeField] private TextMeshProUGUI bossName;

    private void Start()
    {
        stats = GetComponentInParent<EntityStats>();

        healthSlider.maxValue = stats.health.GetValue();
        easeHealthSlider.maxValue = stats.health.GetValue();
        bossName.text = stats.entityName;
    }

    private void Update()
    {
        healthSlider.value = stats.currentHealth;
        easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, stats.currentHealth, 0.05f);
    }
}
