using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiSpearAttackState : EnemyState
{
    private readonly SamuraiSpear enemy;
    public SamuraiSpearAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, SamuraiSpear _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        if(triggerCalled)
            stateMachine.ChangeState(enemy.Chase);
        
    }
}
