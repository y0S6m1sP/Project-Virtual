using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiSpearIdleState : EnemyState
{
    private readonly SamuraiSpear enemy;

    public SamuraiSpearIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, SamuraiSpear _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }
}