using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private GameObject healthPrefab;
    [SerializeField] private GameObject armorPrefab;
    [SerializeField] private EntityStats playerStats;
    private GridLayoutGroup healthBar;

    void Start()
    {
        healthBar = GetComponentInChildren<GridLayoutGroup>();
        playerStats.onHealthChanged += UpdateHealthBar;
        playerStats.onArmorChanged += UpdateHealthBar;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        int currentHearts = playerStats.currentHealth;
        int currentArmor = playerStats.currentArmor;

        foreach (Transform child in healthBar.transform)
        {
            Destroy(child.gameObject);
        }

        for (int i = 0; i < currentHearts; i++)
        {
            Instantiate(healthPrefab, healthBar.transform);
        }

        for (int i = 0; i < currentArmor; i++)
        {
            Instantiate(armorPrefab, healthBar.transform);
        }
    }
}
