using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_RuneToolTip : UI_ToolTip
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemRarityText;
    [SerializeField] private TextMeshProUGUI itemDescription;

    public void ShowToolTip(ItemDataRune item)
    {
        if (item == null)
            return;

        itemNameText.text = item.itemName;
        itemRarityText.text = item.rarity.ToString();
        itemDescription.text = item.GetDescription();

        UpdatePosition();

        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
    }
}
