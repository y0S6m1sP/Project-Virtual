using System.Collections;
using UnityEngine;

public class EnemyStats : EntityStats
{
    private Enemy enemy;
    
    override protected void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();
    }

    protected override void Die()
    {
        base.Die();
        enemy.Die();
    }
}