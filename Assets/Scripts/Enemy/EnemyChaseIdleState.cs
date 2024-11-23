using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseIdleState : EnemyState
{

    public EnemyChaseIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemyBase, _stateMachine, _animBoolName)
    {

    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        if (enemy.IsPlayerInAttackDistance())
        {
            if (enemy.CanAttack())
                stateMachine.ChangeState(enemy.Attack);
            // else stateMachine.ChangeState(enemy.ChaseIdle);
        }
        else
        {
            stateMachine.ChangeState(enemy.Chase);
        }

    }
}
