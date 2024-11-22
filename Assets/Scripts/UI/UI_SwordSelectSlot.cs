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

    private System.Action onSwordSelected;

    private ItemDataSword sword;

    public void OnPointerDown(PointerEventData eventData)
    {
        SwordManager.Instance.AddSword(sword);
        UIManager.instance.swordSelect.HideSwordSelect();
        onSwordSelected?.Invoke();
    }

    public void SetSword(ItemDataSword sword , System.Action onSwordSelected)
    {
        if (sword == null)
            return;

        this.sword = sword;
        this.onSwordSelected = onSwordSelected;
        itemIcon.sprite = sword.itemIcon;
        itemNameText.text = sword.itemName;
        itemRarityText.text = sword.rarity.ToString();
        itemDescription.text = sword.GetDescription();
    }
}
