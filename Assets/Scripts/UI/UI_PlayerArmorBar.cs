using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerArmorBar : MonoBehaviour
{
    [SerializeField] private GameObject armorPrefab;
    [SerializeField] private EntityStats playerStats;
    private GridLayoutGroup armorBar;

    void Start()
    {
        armorBar = GetComponentInChildren<GridLayoutGroup>();
        playerStats.onArmorChanged += UpdateArmorBar;
        UpdateArmorBar();
    }

    private void UpdateArmorBar()
    {
        int currentHearts = armorBar.transform.childCount;

        while (currentHearts < playerStats.currentArmor)
        {
            Instantiate(armorPrefab, armorBar.transform);
            currentHearts++;
        }

        while (currentHearts > playerStats.currentArmor)
        {
            Destroy(armorBar.transform.GetChild(currentHearts - 1).gameObject);
            currentHearts--;
        }
    }
}
