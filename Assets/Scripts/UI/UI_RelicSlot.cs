using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_RelicSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Image icon;

    private ItemData relic;

    public void AddRelic(ItemData _relic)
    {
        relic = _relic;
        icon.sprite = relic.itemIcon;
        icon.color = Color.white;
    }

    public void CleanUpSlot()
    {
        relic = null;
        icon.sprite = null;
        icon.color = Color.clear;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIManager.instance.relicTooltip.ShowToolTip((ItemDataRelic)relic);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.instance.relicTooltip.HideToolTip();
    }
}
