using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerFocusSlot : MonoBehaviour
{
    private Image focus;

    private void Start() {
        focus = GetComponent<Image>();
    }

    public void SetFocus(bool isFocus)
    {
        if (isFocus)
        {
            focus.sprite = Resources.Load<Sprite>("UI/focus1");
        }
        else
        {
            focus.sprite = Resources.Load<Sprite>("UI/focus0");
        }
    }
}