using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSpecial2State : EnemyState
{

    public EnemyAttackSpecial2State(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.BaseEnemyAttackSpecial2Instance.DoEnter();
    }

    public override void Update()
    {
        base.Update();
        enemy.BaseEnemyAttackSpecial2Instance.DoUpdate();

    }
    public override void Exit()
    {
        base.Exit();
        enemy.BaseEnemyAttackSpecial2Instance.DoExit();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        enemy.BaseEnemyAttackSpecial2Instance.DoAniamtionFinishTrigger();
    }
}
