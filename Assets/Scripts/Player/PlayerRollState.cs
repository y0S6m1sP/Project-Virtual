using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRollState : PlayerState
{
    public PlayerRollState(Player _player, PlayerStateMachine _stateMachine, string _animParamName) : base(_player, _stateMachine, _animParamName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.rollDuration;
    }

    override public void Exit()
    {
        base.Exit();

        // player.SetVelocity(rb.velocity.x, rb.velocity.y);
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(player.rollSpeed * player.FacingDir, rb.velocity.y);

        if (stateTimer < 0)
            if (player.IsGroundDetected())
                stateMachine.ChangeState(player.Idle);
            else
                stateMachine.ChangeState(player.Fall);

    }
}