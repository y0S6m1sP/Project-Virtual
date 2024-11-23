using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyGroundState
{
    public EnemyIdleState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.BaseEnemyIdleInstance.DoEnter();
    }

    public override void Update()
    {
        base.Update();
        enemy.BaseEnemyIdleInstance.DoUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.BaseEnemyIdleInstance.DoExit();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        enemy.BaseEnemyIdleInstance.DoAniamtionFinishTrigger();
    }
}