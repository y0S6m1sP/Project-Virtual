using UnityEngine;

public class UI_PlayerFocusBar : MonoBehaviour {

    [SerializeField] private EntityStats stats;
    [SerializeField] private UI_PlayerFocusSlot[] focusSlots;

    private void Start() {
        focusSlots = transform.GetComponentsInChildren<UI_PlayerFocusSlot>();

        stats.onFocusChanged += SetFocus;
    }

    public void SetFocus() {
        for (int i = 0; i < focusSlots.Length; i++) {
            if (i < stats.currentFocus) {
                focusSlots[i].SetFocus(true);
            } else {
                focusSlots[i].SetFocus(false);
            }
        }
    }

    private void OnDisable() {
        stats.onFocusChanged -= SetFocus;
    }
}