using UnityEngine;

public abstract class UI_ToolTip : MonoBehaviour
{
    [SerializeField] private Camera uiCamera;

    private void FixedUpdate()
    {
        UpdatePosition();
    }

    public void UpdatePosition()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), Input.mousePosition, uiCamera, out Vector2 localPoint);
        transform.localPosition = localPoint;
    }
}
