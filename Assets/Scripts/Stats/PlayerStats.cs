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

    public override void DoDamage(EntityStats _entityStats)
    {
        base.DoDamage(_entityStats);
        Fx.CreateHitFX(_entityStats.transform);
    }

    protected override void Die()
    {
        base.Die();
        player.Die();
    }
}