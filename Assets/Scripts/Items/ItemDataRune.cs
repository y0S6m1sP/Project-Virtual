using UnityEngine;

[CreateAssetMenu(fileName = "New Rune Data", menuName = "Data/Rune")]
public class ItemDataRune : ItemData
{
    [Header("Rune stats")]
    public int brutal;
    public int survive;
    public int mystic;

    public void AddModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.brutal.AddModifier(brutal);
        playerStats.survive.AddModifier(survive);
        playerStats.mystic.AddModifier(mystic);
    }

    public void RemoveModifiers()
    {
        PlayerStats playerStats = PlayerManager.instance.player.GetComponent<PlayerStats>();

        playerStats.brutal.AddModifier(brutal);
        playerStats.survive.AddModifier(survive);
        playerStats.mystic.AddModifier(mystic);
    }

}