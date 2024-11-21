using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public UI_SwordSlot CurrentSlot { get; set; }

    private void Awake()
    {
        canvas = GetComponentInParent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
        SwordManager.Instance.BlocksAllRaycasts(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        SwordManager.Instance.BlocksAllRaycasts(true);

        if (CurrentSlot != null)
        {
            rectTransform.anchoredPosition = CurrentSlot.GetComponent<RectTransform>().anchoredPosition;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SwordManager.Instance.BlocksAllRaycasts(true);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SwordManager.Instance.swordSlotDict.TryGetValue(CurrentSlot, out var sword);
        if (sword != null)
            UIManager.instance.swordTooltip.ShowToolTip(sword);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIManager.instance.swordTooltip.HideToolTip();
    }
}