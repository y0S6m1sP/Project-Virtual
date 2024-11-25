using UnityEngine;

public class CampFireController : MonoBehaviour
{

    private bool canInteract = false;

    private void Update()
    {
        if (canInteract && Input.GetKeyDown(KeyCode.E))
        {
            var playerStats = PlayerManager.instance.player.Stats;
            playerStats.IncreaseHealthBy(playerStats.maxHealth.GetValue() - playerStats.currentHealth);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        canInteract = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        canInteract = false;
    }


}