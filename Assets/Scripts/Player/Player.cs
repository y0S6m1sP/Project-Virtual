using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public float moveSpeed;

    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState Idle { get; private set; }
    public PlayerMoveState Move { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new PlayerStateMachine();
        Idle = new PlayerIdleState(this, StateMachine, "Idle");
        Move = new PlayerMoveState(this, StateMachine, "Move");
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.Initialize(Idle);
    }

    protected override void Update()
    {
        base.Update();
        StateMachine.CurrentState.Update();
    }
}
