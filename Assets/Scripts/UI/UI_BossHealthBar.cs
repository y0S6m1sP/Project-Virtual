using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UI_BossHealthBar : MonoBehaviour
{
    private EntityStats myStats;
    private RectTransform myTransform;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider easeHealthSlider;

    private void Start()
    {
        myTransform = GetComponent<RectTransform>();
        myStats = GetComponentInParent<EntityStats>();

        healthSlider.maxValue = myStats.maxHealth.GetValue();
        easeHealthSlider.maxValue = myStats.maxHealth.GetValue();
    }

    private void Update()
    {
        healthSlider.value = myStats.currentHealth;
        easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, myStats.currentHealth, 0.05f);
    }
}
