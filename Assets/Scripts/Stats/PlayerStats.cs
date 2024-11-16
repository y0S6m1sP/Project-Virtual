using System.Collections;
using UnityEngine;

public class PlayerStats : EntityStats
{
    private Player player;
    
    override protected void Start()
    {
        base.Start();
        player = GetComponent<Player>();
    }

    protected override void Die()
    {
        base.Die();
        player.Die();
    }
}