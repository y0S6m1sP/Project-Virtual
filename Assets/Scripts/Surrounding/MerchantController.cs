using Unity.VisualScripting;
using UnityEngine;

public class MerchantController : InteractableController
{
    protected override void Update()
    {
        if (isInteractable && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Open merchant UI");
        }
    }

}