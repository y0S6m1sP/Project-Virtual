using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyGroundState
{
    public EnemyMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemyBase, _stateMachine, _animBoolName)
    {

    }

    override public void Enter()
    {
        base.Enter();
        // stateTimer = enemy.moveTime;
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(enemy.moveSpeed * enemy.FacingDir, rb.velocity.y);

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
            enemy.Flip();

        // if (stateTimer < 0)
        //     stateMachine.ChangeState(enemy.Idle);
    }

}
