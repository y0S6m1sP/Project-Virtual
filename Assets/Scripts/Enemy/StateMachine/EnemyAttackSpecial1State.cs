using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackSpecial1State : EnemyState
{

    public EnemyAttackSpecial1State(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName) : base(_enemy, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemy.BaseEnemyAttackSpecial1Instance.DoEnter();
    }

    public override void Update()
    {
        base.Update();
        enemy.BaseEnemyAttackSpecial1Instance.DoUpdate();

    }
    public override void Exit()
    {
        base.Exit();
        enemy.BaseEnemyAttackSpecial1Instance.DoExit();
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
        enemy.BaseEnemyAttackSpecial1Instance.DoAniamtionFinishTrigger();
    }
}
