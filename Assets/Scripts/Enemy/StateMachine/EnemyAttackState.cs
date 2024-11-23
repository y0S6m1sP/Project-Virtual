using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : EnemyState
{

    public EnemyAttackState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.BaseEnemyAttackInstance.DoEnter();
    }

    public override void Update()
    {
        base.Update();
        enemy.BaseEnemyAttackInstance.DoUpdate();

    }
    public override void Exit()
    {
        base.Exit();
        enemy.BaseEnemyAttackInstance.DoExit();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        enemy.BaseEnemyAttackInstance.DoAniamtionFinishTrigger();
    }
}
