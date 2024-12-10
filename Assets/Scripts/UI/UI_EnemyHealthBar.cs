using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_EnemyHealthBar : MonoBehaviour
{

    private Entity entity;
    private EntityStats stats;
    private RectTransform mTransform;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider easeHealthSlider;

    private void Start()
    {
        mTransform = GetComponent<RectTransform>();
        entity = GetComponentInParent<Entity>();
        stats = GetComponentInParent<EntityStats>();

        healthSlider.maxValue = stats.health.GetValue();
        easeHealthSlider.maxValue = stats.health.GetValue();
        entity.onFlipped += FlipUI;
    }

    private void Update()
    {
        healthSlider.value = stats.currentHealth;
        easeHealthSlider.value = Mathf.Lerp(easeHealthSlider.value, stats.currentHealth, 0.05f);

        if (stats.currentHealth <= 0)
        {
            Destroy(gameObject, 0.3f);
        }
    }

    private void FlipUI() => mTransform.Rotate(0, 180, 0);

    private void OnDisable()
    {
        entity.onFlipped -= FlipUI;
    }
}
