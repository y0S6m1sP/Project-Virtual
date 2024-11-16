using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerHealthBar : MonoBehaviour
{
    [SerializeField] private GameObject healthPrefab;
    [SerializeField] private EntityStats playerStats;
    private GridLayoutGroup healthBar;

    void Start()
    {
        healthBar = GetComponentInChildren<GridLayoutGroup>();
        playerStats.onHealthChanged += UpdateHealthBar;
        UpdateHealthBar();
    }

    private void UpdateHealthBar()
    {
        int currentHearts = healthBar.transform.childCount;

        while (currentHearts < playerStats.currentHealth)
        {
            Instantiate(healthPrefab, healthBar.transform);
            currentHearts++;
        }

        while (currentHearts > playerStats.currentHealth)
        {
            Destroy(healthBar.transform.GetChild(currentHearts - 1).gameObject);
            currentHearts--;
        }
    }
}
