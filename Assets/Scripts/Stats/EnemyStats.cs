using System.Collections;
using UnityEngine;
using Cinemachine;

public class EnemyStats : EntityStats
{
    private Enemy enemy;

    override protected void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        
        AudioManager.instance.PlaySFX(Random.Range(3, 5));
    }

    protected override void Die()
    {
        base.Die();
        enemy.Die();
    }
}