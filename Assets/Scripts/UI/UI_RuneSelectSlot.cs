using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_RuneSelectSlot : MonoBehaviour, IPointerDownHandler
{

    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemRarityText;
    [SerializeField] private TextMeshProUGUI itemDescription;

    private System.Action onRuneSelected;

    private ItemDataRune rune;

    public void OnPointerDown(PointerEventData eventData)
    {
        RuneManager.Instance.AddRune(rune);
        UIManager.instance.runeSelect.HideRuneSelect();
        onRuneSelected?.Invoke();
    }

    public void SetRune(ItemDataRune rune, System.Action onRuneSelected)
    {
        if (rune == null)
            return;

        this.rune = rune;
        this.onRuneSelected = onRuneSelected;
        itemIcon.sprite = rune.itemIcon;
        itemNameText.text = rune.itemName;
        itemRarityText.text = rune.rarity.ToString();
        itemDescription.text = rune.GetDescription();
    }
}
