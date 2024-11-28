using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSpecial3State : EnemyState
{

    public EnemyAttackSpecial3State(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.BaseEnemyAttackSpecial3Instance.DoEnter();
    }

    public override void Update()
    {
        base.Update();
        enemy.BaseEnemyAttackSpecial3Instance.DoUpdate();

    }
    public override void Exit()
    {
        base.Exit();
        enemy.BaseEnemyAttackSpecial3Instance.DoExit();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        enemy.BaseEnemyAttackSpecial3Instance.DoAniamtionFinishTrigger();
    }
}
