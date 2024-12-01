using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        // if (Input.GetKeyDown(KeyCode.Mouse0))
        //     stateMachine.ChangeState(player.Attack1);

        if (Input.GetKeyDown(KeyCode.Mouse1))
            stateMachine.ChangeState(player.Parry);

        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.Fall);

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            stateMachine.ChangeState(player.Jump);

        if (Input.GetKeyDown(KeyCode.LeftShift))
            stateMachine.ChangeState(player.Roll);
    }

}