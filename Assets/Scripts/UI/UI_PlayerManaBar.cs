using System;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerManaBar : MonoBehaviour
{
    private EntityStats stats;
    [SerializeField] private Slider manaSlider;

    private void Start()
    {
        stats = GetComponentInParent<EntityStats>();

        manaSlider.maxValue = stats.mana.GetValue();
    }

    private void Update()
    {
        manaSlider.value = stats.currentMana;
    }
}
