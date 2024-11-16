using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SamuraiSpear : Enemy
{

    public SamuraiSpearIdleState Idle { get; private set; }
    public SamuraiSpearMoveState Move { get; private set; }
    public SamuraiSpearChaseState Chase { get; private set; }
    public SamuraiSpearChaseIdleState ChaseIdle { get; private set; }
    public SamuraiSpearAttackState Attack { get; private set; }
    public SamuraiSpearDeadState Dead { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Idle = new SamuraiSpearIdleState(this, StateMachine, "Idle", this);
        Move = new SamuraiSpearMoveState(this, StateMachine, "Move", this);
        Attack = new SamuraiSpearAttackState(this, StateMachine, "Attack", this);
        Chase = new SamuraiSpearChaseState(this, StateMachine, "Move", this);
        ChaseIdle = new SamuraiSpearChaseIdleState(this, StateMachine, "Idle", this);
        Dead = new SamuraiSpearDeadState(this, StateMachine, "Dead", this);
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(Idle);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Die()
    {
        base.Die();
        StateMachine.ChangeState(Dead);
    }
}