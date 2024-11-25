using UnityEngine;

public class CampFireController : MonoBehaviour {

     private bool canInteract = false;

    private void Update() {
        if (canInteract && Input.GetKeyDown(KeyCode.E)) {
            Debug.Log("Heal Player and make fire off?");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        canInteract = true;
    }

    private void OnTriggerExit2D(Collider2D other) {
        canInteract = false;
    }
    
    
}