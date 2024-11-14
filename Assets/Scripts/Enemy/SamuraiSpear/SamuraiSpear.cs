using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SamuraiSpear : Enemy
{

    public SamuraiSpearIdleState Idle { get; private set; }
    public SamuraiSpearMoveState Move { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        Idle = new SamuraiSpearIdleState(this, StateMachine, "Idle", this);
        Move = new SamuraiSpearMoveState(this, StateMachine, "Move", this);
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
}