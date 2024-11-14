using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiSpearChaseState : EnemyState
{

    private readonly SamuraiSpear enemy;
    private Transform player;
    private int chaseDir;

    public SamuraiSpearChaseState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, SamuraiSpear _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = PlayerManager.instance.player.transform;
    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.chaseTime;

            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if (enemy.CanAttack())
                    stateMachine.ChangeState(enemy.Attack);
                else stateMachine.ChangeState(enemy.ChaseIdle);
            }
        }
        else
        {
            if (stateTimer < 0)
                stateMachine.ChangeState(enemy.Idle);
        }

        if (player.position.x > enemy.transform.position.x)
            chaseDir = 1;
        else if (player.position.x < enemy.transform.position.x)
            chaseDir = -1;

        enemy.SetVelocity(enemy.moveSpeed * chaseDir, rb.velocity.y);
    }
}
