using UnityEngine;

public class CampFireController : InteractableController
{

    protected override void Start()
    {
        Sr = GameObject.Find("Roaster").GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        if (isInteractable && Input.GetKeyDown(KeyCode.E))
        {
            var playerStats = PlayerManager.instance.player.Stats;
            playerStats.IncreaseHealthBy(playerStats.health.GetValue() - playerStats.currentHealth);
        }
    }

}