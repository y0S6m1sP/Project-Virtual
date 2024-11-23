using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{

    protected EnemyStateMachine stateMachine;
    protected Enemy enemy;
    protected Rigidbody2D rb;

    private readonly string animBoolName;

    public EnemyState(Enemy _enemy, EnemyStateMachine _stateMachine, string _animBoolName)
    {
        enemy = _enemy;
        stateMachine = _stateMachine;
        animBoolName = _animBoolName;
    }

    public virtual void Update()
    {

    }

    public virtual void Enter()
    {
        rb = enemy.Rb;
        enemy.Anim.SetBool(animBoolName, true);
    }

    public virtual void Exit()
    {
        enemy.Anim.SetBool(animBoolName, false);
    }

    public virtual void AnimationFinishTrigger()
    {

    }
}
