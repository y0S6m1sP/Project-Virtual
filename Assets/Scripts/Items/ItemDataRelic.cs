using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public enum RelicRarity
{
    Common,
    Rare,
    Epic,
    Legendary,
}


[CreateAssetMenu(fileName = "New Relic Data", menuName = "Data/Relic")]
public class ItemDataRelic : ItemData
{
    public RelicRarity rarity;

    [Header("Relic stats")]
    public int maxHealth;
    public int damage;
    public int armor;

    public void AddModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.maxHealth.AddModifier(maxHealth);
        playerStats.damage.AddModifier(damage);
        playerStats.armor.AddModifier(armor);
    }

    public void RemoveModifiers(){
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();
        
        playerStats.maxHealth.RemoveModifier(maxHealth);
        playerStats.damage.RemoveModifier(damage);
        playerStats.armor.RemoveModifier(armor);
    }

}