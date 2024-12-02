using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_RuneSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image icon;

    private ItemData rune;

    public void AddRune(ItemData _rune)
    {
        rune = _rune;
        icon.sprite = rune.itemIcon;
        icon.color = Color.white;
    }

    public void CleanUpSlot()
    {
        rune = null;
        icon.sprite = null;
        icon.color = Color.clear;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.instance.runeTooltip.ShowToolTip((ItemDataRune)rune);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.instance.runeTooltip.HideToolTip();
    }
}
