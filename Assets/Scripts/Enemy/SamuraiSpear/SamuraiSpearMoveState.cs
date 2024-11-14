using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiSpearMoveState : SamuraiSpearGroundState
{

    private readonly SamuraiSpear enemy;

    public SamuraiSpearMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, SamuraiSpear _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    override public void Enter()
    {
        base.Enter();
        stateTimer = enemy.moveTime;
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(enemy.moveSpeed * enemy.FacingDir, rb.velocity.y);

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected())
        {
            enemy.Flip();
        }

        if (stateTimer < 0)
            stateMachine.ChangeState(enemy.Idle);
    }

}
