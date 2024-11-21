using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_SwordToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemRarityText;
    [SerializeField] private TextMeshProUGUI itemDescription;

    public void ShowToolTip(ItemDataSword item)
    {
        if (item == null)
            return;

        itemNameText.text = item.itemName;
        itemRarityText.text = item.rarity.ToString();
        itemDescription.text = item.GetDescription();

        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }
}
