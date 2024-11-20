using UnityEngine;
using UnityEngine.EventSystems;

public class UI_SwordSlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null && eventData.pointerDrag.TryGetComponent<DragDrop>(out var dragDrop))
        {
            UI_SwordSlot originalSlot = dragDrop.CurrentSlot;
            RectTransform droppedRectTransform = eventData.pointerDrag.GetComponent<RectTransform>();

            if (SwordManager.Instance.swordSlotDict[this] != null && SwordManager.Instance.swordSlotDict[originalSlot] != null)
            {
                (SwordManager.Instance.swordSlotDict[originalSlot], SwordManager.Instance.swordSlotDict[this]) = (SwordManager.Instance.swordSlotDict[this], SwordManager.Instance.swordSlotDict[originalSlot]);
                (SwordManager.Instance.dragDropDict[originalSlot], SwordManager.Instance.dragDropDict[this]) = (SwordManager.Instance.dragDropDict[this], SwordManager.Instance.dragDropDict[originalSlot]);

                droppedRectTransform.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;
                SwordManager.Instance.dragDropDict[originalSlot].GetComponent<RectTransform>().anchoredPosition = originalSlot.GetComponent<RectTransform>().anchoredPosition;

                dragDrop.CurrentSlot = this;
                SwordManager.Instance.dragDropDict[originalSlot].CurrentSlot = originalSlot;
            }
            else
            {
                droppedRectTransform.anchoredPosition = GetComponent<RectTransform>().anchoredPosition;

                SwordManager.Instance.swordSlotDict[this] = SwordManager.Instance.swordSlotDict[originalSlot];
                SwordManager.Instance.swordSlotDict[originalSlot] = null;

                SwordManager.Instance.dragDropDict[this] = dragDrop;
                SwordManager.Instance.dragDropDict[originalSlot] = null;

                dragDrop.CurrentSlot = this;
            }
        }
    }
}