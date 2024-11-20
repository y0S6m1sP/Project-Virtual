using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
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
        canvasGroup.blocksRaycasts = false;
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
    }
}