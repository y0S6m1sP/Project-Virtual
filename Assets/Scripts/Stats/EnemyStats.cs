using System.Collections;
using UnityEngine;
using Cinemachine;

public class EnemyStats : EntityStats
{
    private Enemy enemy;

    private CinemachineImpulseSource impulseSource;

    override protected void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);

        CameraShakeManager.Instance.CameraShake(impulseSource);
    }

    protected override void Die()
    {
        base.Die();
        enemy.Die();
    }
}