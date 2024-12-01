using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerAirState : PlayerState
{

    int jumpCounter;

    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.jumpCantMoveDuration;
        jumpCounter = player.Anim.GetInteger("JumpCounter");
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (xInput != 0 && stateTimer < 0)
            player.SetVelocity(player.moveSpeed * .8f * xInput, rb.velocity.y);


        if (player.IsGroundDetected())
            player.Anim.SetInteger("JumpCounter", 0);

        if (Input.GetKeyDown(KeyCode.Space) && jumpCounter < 1)
        {
            player.Anim.SetInteger("JumpCounter", jumpCounter + 1);
            stateMachine.ChangeState(player.Jump);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
            stateMachine.ChangeState(player.Roll);

        // if (Input.GetKeyDown(KeyCode.Mouse0))
        //     stateMachine.ChangeState(player.AirAttack);

        if (Input.GetKeyDown(KeyCode.Mouse1))
            stateMachine.ChangeState(player.Parry);

    }
}
