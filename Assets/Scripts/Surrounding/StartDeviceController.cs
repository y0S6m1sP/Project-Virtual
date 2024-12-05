using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartDeviceController : TriggerableSurroundingController
{

    private bool canStart = false;
    private void Update()
    {
        if (canStart && Input.GetKeyDown(KeyCode.E))
        {
            // GameManager.Instance.NextLevel(0f);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        canStart = true;
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        base.OnTriggerExit2D(other);
        canStart = false;
    }
}
