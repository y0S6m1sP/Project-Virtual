using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : EnemyState
{

    public EnemyMoveState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        enemy.BaseEnemyMoveInstance.DoEnter();
    }

    public override void Update()
    {
        base.Update();
        enemy.BaseEnemyMoveInstance.DoUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.BaseEnemyMoveInstance.DoExit();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        enemy.BaseEnemyMoveInstance.DoAniamtionFinishTrigger();
    }
}
