using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_SwordToolTip : MonoBehaviour
{
    [SerializeField] private Camera uiCamera;
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemRarityText;
    [SerializeField] private TextMeshProUGUI itemDescription;

    private void FixedUpdate()
    {
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out Vector2 localPoint);
        transform.localPosition = localPoint;
    }

    public void ShowToolTip(ItemDataSword item)
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
