using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SwordSelectSlot : MonoBehaviour, IPointerDownHandler
{

    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemRarityText;
    [SerializeField] private TextMeshProUGUI itemDescription;

    private ItemDataSword sword;

    public void OnPointerDown(PointerEventData eventData)
    {
        SwordManager.Instance.AddSword(sword);
        UIManager.instance.swordSelect.HideSwordSelect();
    }

    public void SetSword(ItemDataSword sword)
    {
        if (sword == null)
            return;

        this.sword = sword;
        itemIcon.sprite = sword.itemIcon;
        itemNameText.text = sword.itemName;
        itemRarityText.text = sword.rarity.ToString();
        itemDescription.text = sword.GetDescription();
    }
}
