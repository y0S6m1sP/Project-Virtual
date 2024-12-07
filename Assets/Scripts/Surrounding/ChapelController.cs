using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapelController : InteractableController
{
    private bool isSelected = false;
    protected override void Update()
    {
        if (isInteractable && Input.GetKeyDown(KeyCode.E))
        {
            if (isSelected) return;

            UIManager.instance.runeSelect.ShowRuneSelect(() =>
            {
                isSelected = true;
                UIManager.instance.runeSelect.HideRuneSelect();
                UIManager.instance.ShowMap(true);
            });
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        UIManager.instance.ShowMap(false);
    }
}
