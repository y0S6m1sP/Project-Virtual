using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChaseState : EnemyState
{

    public EnemyChaseState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        enemy.BaseEnemyChaseInstance.DoEnter();
    }

    public override void Update()
    {
        base.Update();
        enemy.BaseEnemyChaseInstance.DoUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.BaseEnemyChaseInstance.DoExit();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        enemy.BaseEnemyChaseInstance.DoAniamtionFinishTrigger();
    }
}
