using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{

    public bool IsBusy { get; private set; }

    public float moveSpeed;
    public float jumpForce;

    public PlayerStateMachine StateMachine { get; private set; }

    public PlayerIdleState Idle { get; private set; }
    public PlayerMoveState Move { get; private set; }
    public PlayerAttack1State Attack1 { get; private set; }
    public PlayerAttack2State Attack2 { get; private set; }
    public PlayerJumpState Jump { get; private set; }
    public PlayerAirState Air { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        StateMachine = new PlayerStateMachine();
        Idle = new PlayerIdleState(this, StateMachine, "Idle");
        Move = new PlayerMoveState(this, StateMachine, "Move");
        Attack1 = new PlayerAttack1State(this, StateMachine, "Attack1");
        Attack2 = new PlayerAttack2State(this, StateMachine, "Attack2");
        Jump = new PlayerJumpState(this, StateMachine, "Jump");
        Air = new PlayerAirState(this, StateMachine, "Jump");
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

    public IEnumerator BusyFor(float _seconds)
    {
        IsBusy = true;
        yield return new WaitForSeconds(_seconds);
        IsBusy = false;
    }

    public void AnimFinishTrigger()
    {
        StateMachine.CurrentState.AnimFinishTrigger();
    }

    public void AnimAllowCancel()
    {
        StateMachine.CurrentState.AnimAllowCancel();
    }

}
