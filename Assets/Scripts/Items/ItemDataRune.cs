using UnityEngine;

[CreateAssetMenu(fileName = "New Rune Data", menuName = "Data/Rune")]
public class ItemDataRune : ItemData
{
    [Header("Rune stats")]
    public int health;
    public int mana;
    public int physicDamage;
    public int magicDamage;

    [Space]
    public int brutal;
    public int survive;
    public int mystic;

    public void AddModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.health.AddModifier(health);
        playerStats.mana.AddModifier(mana);
        playerStats.physicDamage.AddModifier(physicDamage);
        playerStats.magicDamage.AddModifier(magicDamage);
        playerStats.brutal.AddModifier(brutal);
        playerStats.survive.AddModifier(survive);
        playerStats.mystic.AddModifier(mystic);
    }

    public void RemoveModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.health.RemoveModifier(health);
        playerStats.mana.RemoveModifier(mana);
        playerStats.physicDamage.RemoveModifier(physicDamage);
        playerStats.magicDamage.RemoveModifier(magicDamage);
        playerStats.brutal.RemoveModifier(brutal);
        playerStats.survive.RemoveModifier(survive);
        playerStats.mystic.RemoveModifier(mystic);
    }

}