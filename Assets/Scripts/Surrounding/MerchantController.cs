using Unity.VisualScripting;
using UnityEngine;

public class MerchantController : MonoBehaviour {

    private bool canInteract = false;

    private void Update() {
        if (canInteract && Input.GetKeyDown(KeyCode.E)) {
            Debug.Log("Open merchant UI");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        canInteract = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        canInteract = false;
    }
    
}