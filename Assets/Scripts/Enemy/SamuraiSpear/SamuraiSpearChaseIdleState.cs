using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiSpearChaseIdleState : EnemyState
{
    private readonly SamuraiSpear enemy;

    public SamuraiSpearChaseIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, SamuraiSpear _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        if (enemy.IsPlayerInAttackDistance())
        {
            if (enemy.CanAttack())
                stateMachine.ChangeState(enemy.Attack);
            else stateMachine.ChangeState(enemy.ChaseIdle);
        }
        else
        {
            stateMachine.ChangeState(enemy.Chase);
        }

    }
}
