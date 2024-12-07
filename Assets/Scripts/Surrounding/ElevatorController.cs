using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : InteractableController
{
    protected override void Update()
    {
        if (isInteractable && Input.GetKeyDown(KeyCode.E))
        {
            UIManager.instance.ShowMap(true);
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        UIManager.instance.ShowMap(false);
    }
}
